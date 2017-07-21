using MvcSiteMapProvider;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Collections;
using MvcSiteMapProvider.Collections.Specialized;
using MvcSiteMapProvider.DI;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Loader;
using MvcSiteMapProvider.Matching;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.Security;
using MvcSiteMapProvider.Visitor;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Compilation;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Xml;
using SCA.WebControls;
using SQFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages.Razor;

namespace LojaProduto.Presentation
{
    public class SCASiteMapConfig
    {
        public static void Initialize()
        {
            WebPageRazorHost.AddGlobalImport("MvcSiteMapProvider.Web.Html");
            WebPageRazorHost.AddGlobalImport("MvcSiteMapProvider.Web.Html.Models");

            var settings = new ConfigurationSettings();

            if (settings.EnableSiteMapFile)
            {
                var validator = new SiteMapXmlValidator();
                validator.ValidateXml(HostingEnvironment.MapPath(settings.SiteMapFileName));
            }

            if (DependencyResolver.Current.GetType().FullName.Equals("System.Web.Mvc.DependencyResolver+DefaultDependencyResolver"))
            {
                var currentFactory = ControllerBuilder.Current.GetControllerFactory();
                ControllerBuilder.Current.SetControllerFactory(
                    new ControllerFactoryDecorator(currentFactory, settings));
            }
            else
            {
                var currentResolver = DependencyResolver.Current;
                DependencyResolver.SetResolver(new DependencyResolverDecorator(currentResolver, settings));
            }

            var siteMapLoaderContainer = new CustomSiteMapLoaderContainer(settings);
            SiteMaps.Loader = siteMapLoaderContainer.ResolveSiteMapLoader();

            if (settings.EnableSitemapsXml)
                XmlSiteMapController.RegisterRoutes(RouteTable.Routes);
        }

        private class CustomSiteMapLoaderContainer
        {
            public CustomSiteMapLoaderContainer(ConfigurationSettings settings)
            {
                if (settings.EnableSiteMapFile)
                    this.absoluteFileName = HostingEnvironment.MapPath(settings.SiteMapFileName);

                this.mvcContextFactory = new MvcContextFactory();
                this.siteMapCache = new SiteMapCache(new RuntimeCacheProvider<ISiteMap>(System.Runtime.Caching.MemoryCache.Default));
                this.cacheDependency = this.ResolveCacheDependency(settings);
                this.requestCache = this.mvcContextFactory.GetRequestCache();
                this.bindingFactory = new BindingFactory();
                this.bindingProvider = new BindingProvider(this.bindingFactory, this.mvcContextFactory);
                this.urlPath = new UrlPath(this.mvcContextFactory, this.bindingProvider);
                this.siteMapCacheKeyGenerator = new SiteMapCacheKeyGenerator(this.mvcContextFactory);
                this.siteMapCacheKeyToBuilderSetMapper = new SiteMapCacheKeyToBuilderSetMapper();
                this.reservedAttributeNameProvider = new ReservedAttributeNameProvider(settings.AttributesToIgnore);
                var siteMapNodeFactoryContainer = new SiteMapNodeFactoryContainer(settings, this.mvcContextFactory, this.urlPath, this.reservedAttributeNameProvider);
                this.siteMapNodeToParentRelationFactory = new SiteMapNodeToParentRelationFactory();
                this.nodeKeyGenerator = new NodeKeyGenerator();
                this.siteMapNodeFactory = siteMapNodeFactoryContainer.ResolveSiteMapNodeFactory();
                this.siteMapNodeCreatorFactory = this.ResolveSiteMapNodeCreatorFactory();
                this.cultureContextFactory = new CultureContextFactory();
                this.dynamicSiteMapNodeBuilderFactory = new DynamicSiteMapNodeBuilderFactory(this.siteMapNodeCreatorFactory, this.cultureContextFactory);
                this.siteMapHierarchyBuilder = new SiteMapHierarchyBuilder();
                this.siteMapNodeHelperFactory = this.ResolveSiteMapNodeHelperFactory();
                this.siteMapNodeVisitor = this.ResolveSiteMapNodeVisitor(settings);
                this.siteMapXmlNameProvider = new SiteMapXmlNameProvider();
                this.attributeAssemblyProviderFactory = new AttributeAssemblyProviderFactory();
                this.mvcSiteMapNodeAttributeDefinitionProvider = new MvcSiteMapNodeAttributeDefinitionProvider();
                this.siteMapNodeProvider = this.ResolveSiteMapNodeProvider(settings);
                this.siteMapBuiderSetStrategy = this.ResolveSiteMapBuilderSetStrategy(settings);
                var siteMapFactoryContainer = new CustomSiteMapFactoryContainer(settings, this.mvcContextFactory, this.urlPath);
                this.siteMapFactory = siteMapFactoryContainer.ResolveSiteMapFactory();
                this.siteMapCreator = new SiteMapCreator(this.siteMapCacheKeyToBuilderSetMapper, this.siteMapBuiderSetStrategy, this.siteMapFactory);
            }

            private readonly string absoluteFileName;
            private readonly IMvcContextFactory mvcContextFactory;
            private readonly IBindingFactory bindingFactory;
            private readonly IBindingProvider bindingProvider;
            private readonly ISiteMapCache siteMapCache;
            private readonly ICacheDependency cacheDependency;
            private readonly IRequestCache requestCache;
            private readonly IUrlPath urlPath;
            private readonly ISiteMapCacheKeyGenerator siteMapCacheKeyGenerator;
            private readonly ISiteMapCacheKeyToBuilderSetMapper siteMapCacheKeyToBuilderSetMapper;
            private readonly ISiteMapBuilderSetStrategy siteMapBuiderSetStrategy;
            private readonly INodeKeyGenerator nodeKeyGenerator;
            private readonly ISiteMapNodeToParentRelationFactory siteMapNodeToParentRelationFactory;
            private readonly ISiteMapNodeFactory siteMapNodeFactory;
            private readonly ISiteMapNodeCreatorFactory siteMapNodeCreatorFactory;
            private readonly ISiteMapNodeHelperFactory siteMapNodeHelperFactory;
            private readonly ISiteMapNodeVisitor siteMapNodeVisitor;
            private readonly ISiteMapNodeProvider siteMapNodeProvider;
            private readonly ISiteMapHierarchyBuilder siteMapHierarchyBuilder;
            private readonly IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory;
            private readonly IMvcSiteMapNodeAttributeDefinitionProvider mvcSiteMapNodeAttributeDefinitionProvider;
            private readonly ICultureContextFactory cultureContextFactory;
            private readonly ISiteMapXmlNameProvider siteMapXmlNameProvider;
            private readonly IReservedAttributeNameProvider reservedAttributeNameProvider;
            private readonly IDynamicSiteMapNodeBuilderFactory dynamicSiteMapNodeBuilderFactory;
            private readonly ISiteMapFactory siteMapFactory;
            private readonly ISiteMapCreator siteMapCreator;

            public ISiteMapLoader ResolveSiteMapLoader()
            {
                return new SiteMapLoader(this.siteMapCache, this.siteMapCacheKeyGenerator, this.siteMapCreator);
            }

            private ISiteMapBuilderSetStrategy ResolveSiteMapBuilderSetStrategy(ConfigurationSettings settings)
            {
                return new SiteMapBuilderSetStrategy(
                    new ISiteMapBuilderSet[] {
                    new SiteMapBuilderSet("default", settings.SecurityTrimmingEnabled, settings.EnableLocalization,
                        settings.VisibilityAffectsDescendants, settings.UseTitleIfDescriptionNotProvided,
                        this.ResolveSiteMapBuilder(settings), this.ResolveCacheDetails(settings)) });
            }

            private ISiteMapBuilder ResolveSiteMapBuilder(ConfigurationSettings settings)
            {
                return new SiteMapBuilder(this.siteMapNodeProvider, this.siteMapNodeVisitor, this.siteMapHierarchyBuilder,
                    this.siteMapNodeHelperFactory, this.cultureContextFactory);
            }

            private ISiteMapNodeProvider ResolveSiteMapNodeProvider(ConfigurationSettings settings)
            {
                var providers = new List<ISiteMapNodeProvider>();

                if (settings.EnableSiteMapFile)
                    providers.Add(this.ResolveXmlSiteMapNodeProvider(settings.IncludeRootNodeFromSiteMapFile, settings.EnableSiteMapFileNestedDynamicNodeRecursion));

                if (settings.ScanAssembliesForSiteMapNodes)
                    providers.Add(this.ResolveReflectionSiteMapNodeProvider(settings.IncludeAssembliesForScan, settings.ExcludeAssembliesForScan));

                return new CompositeSiteMapNodeProvider(providers.ToArray());
            }

            private ISiteMapNodeProvider ResolveXmlSiteMapNodeProvider(bool includeRootNode, bool useNestedDynamicNodeRecursion)
            {
                return new XmlSiteMapNodeProvider(includeRootNode, useNestedDynamicNodeRecursion,
                    new FileXmlSource(this.absoluteFileName), this.siteMapXmlNameProvider);
            }

            private ISiteMapNodeProvider ResolveReflectionSiteMapNodeProvider(IEnumerable<string> includeAssemblies, IEnumerable<string> excludeAssemblies)
            {
                return new ReflectionSiteMapNodeProvider(includeAssemblies, excludeAssemblies,
                    this.attributeAssemblyProviderFactory, this.mvcSiteMapNodeAttributeDefinitionProvider);
            }

            private ISiteMapNodeVisitor ResolveSiteMapNodeVisitor(ConfigurationSettings settings)
            {
                if (settings.EnableResolvedUrlCaching)
                    return new UrlResolvingSiteMapNodeVisitor();
                else
                    return new NullSiteMapNodeVisitor();
            }

            private ISiteMapNodeCreatorFactory ResolveSiteMapNodeCreatorFactory()
            {
                return new SiteMapNodeCreatorFactory(this.siteMapNodeFactory, this.nodeKeyGenerator, this.siteMapNodeToParentRelationFactory);
            }

            private ISiteMapNodeHelperFactory ResolveSiteMapNodeHelperFactory()
            {
                return new SiteMapNodeHelperFactory(this.siteMapNodeCreatorFactory, this.dynamicSiteMapNodeBuilderFactory,
                    this.reservedAttributeNameProvider, this.cultureContextFactory);
            }

            private ICacheDetails ResolveCacheDetails(ConfigurationSettings settings)
            {
                return new CacheDetails(TimeSpan.FromMinutes(settings.CacheDuration), TimeSpan.MinValue, this.cacheDependency);
            }

            private ICacheDependency ResolveCacheDependency(ConfigurationSettings settings)
            {
                if (settings.EnableSiteMapFile)
                    return new RuntimeFileCacheDependency(absoluteFileName);
                else
                    return new NullCacheDependency();
            }
        }

        private class CustomSiteMapFactoryContainer
        {
            public CustomSiteMapFactoryContainer(ConfigurationSettings settings, IMvcContextFactory mvcContextFactory, IUrlPath urlPath)
            {
                this.settings = settings;
                this.mvcContextFactory = mvcContextFactory;
                this.requestCache = this.mvcContextFactory.GetRequestCache();
                this.urlPath = urlPath;
                this.urlKeyFactory = new UrlKeyFactory(this.urlPath);
            }

            private readonly ConfigurationSettings settings;
            private readonly IMvcContextFactory mvcContextFactory;
            private readonly IRequestCache requestCache;
            private readonly IUrlPath urlPath;
            private readonly IUrlKeyFactory urlKeyFactory;

            public ISiteMapFactory ResolveSiteMapFactory()
            {
                return new SiteMapFactory(this.ResolveSiteMapPluginProviderFactory(), new MvcResolverFactory(),
                    this.mvcContextFactory, this.ResolveSiteMapChildStateFactory(), this.urlPath,
                    this.ResolveControllerTypeResolverFactory(), new ActionMethodParameterResolverFactory(new ControllerDescriptorFactory()));
            }

            private ISiteMapPluginProviderFactory ResolveSiteMapPluginProviderFactory()
            {
                return new SiteMapPluginProviderFactory(this.ResolveAclModule());
            }

            private IAclModule ResolveAclModule()
            {
                return new CompositeAclModule(new SCASiteMapAclModule());
            }

            private ISiteMapChildStateFactory ResolveSiteMapChildStateFactory()
            {
                return new SiteMapChildStateFactory(new GenericDictionaryFactory(), new SiteMapNodeCollectionFactory(), this.urlKeyFactory);
            }

            private IControllerTypeResolverFactory ResolveControllerTypeResolverFactory()
            {
                return new ControllerTypeResolverFactory(settings.ControllerTypeResolverAreaNamespacesToIgnore,
                    new ControllerBuilderAdapter(ControllerBuilder.Current), new BuildManagerAdapter());
            }
        }

        private class SCASiteMapAclModule : IAclModule
        {
            public bool IsAccessibleToUser(ISiteMap siteMap, ISiteMapNode node)
            {
                if (node == null)
                    throw new ArgumentNullException("node");

                try
                {
                    var usuario = SCAApplicationContext.Usuario;

                    if (usuario != null && usuario.Master)
                        return true;

                    if (string.IsNullOrWhiteSpace(node.Url))
                    {
                        if (node.HasChildNodes)
                        {
                            foreach (var childNode in node.ChildNodes)
                            {
                                bool isAcessible = childNode.IsAccessibleToUser();

                                if (isAcessible)
                                    return true;
                            }
                        }
                        else
                            return false;
                    }

                    var permissoes = SCAApplicationContext.Permissoes;

                    if (permissoes != null && permissoes.PermissoesFuncionalidades != null && permissoes.PermissoesFuncionalidades.Count > 0)
                    {
                        var controller = string.Format("/{0}", node.Controller);
                        var action = string.Format("{0}/{1}", controller, node.Action);

                        return permissoes.PermissoesFuncionalidades
                            .Any(p => p.Value && (p.Key.EqualsIgnoreCase(controller) || p.Key.EqualsIgnoreCase(action)));
                    }
                }
                catch
                {
                }

                return false;
            }
        }
    }
}