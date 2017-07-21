using SQFramework.Web.Mvc.ModelBinder;
using System;
using System.Web.Mvc;

namespace LojaProduto.Presentation
{
    public class ModelBinderConfig
    {
        public static void RegisterAll()
        {
            ModelBinders.Binders.Add(typeof(byte), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(byte?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(decimal), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(double), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(double?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(Int16), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(Int16?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(Int32), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(Int32?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(Int64), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(Int64?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(SByte), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(SByte?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(Single), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(Single?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(UInt16), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(UInt16?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(UInt32), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(UInt32?), new NumericModelBinder());

            ModelBinders.Binders.Add(typeof(UInt64), new NumericModelBinder());
            ModelBinders.Binders.Add(typeof(UInt64?), new NumericModelBinder());
        }
    }
}