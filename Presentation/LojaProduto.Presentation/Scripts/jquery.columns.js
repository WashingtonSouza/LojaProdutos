/*** 
 * Copyright (c) 2014 
 * Licensed under the MIT License.
 *
 * Author: Michael Eisenbraun
 * Version: 2.2.2
 * Requires: jQuery 1.7.2+
 * Documentation: http://eisenbraun.github.io/columns/
 */

if (!window.console) {
    var console = {
        log: function() { }
    };
}

(function($) {
    $.fn.columns = function(options) {
        var val = [];
        var args = Array.prototype.slice.call(arguments, 1);
    
        if (typeof options === 'string') {
            this.each(function() {
            
                var instance = $.data(this, 'columns');
                if (typeof instance !== 'undefined' && $.isFunction(instance[options])) {
                    var methodVal = instance[options].apply(instance, args);
                    if (methodVal !== undefined && methodVal !== instance) {
                        val.push(methodVal);
                    }
                } else {
                    return $.error('No such method "' + options + '" for Columns');
                }
            });
                
        } else {
            this.each(function() {
                if (!$.data(this, 'columns')) {
                    $.data(this, 'columns', new Columns(this, options));
                }
            });
        }
    
        if (val.length === 0) {
            return this.data('columns');
        } else if (val.length === 1) {
            return val[0];
        } else {
            return val;
        }
    };

    var Columns = function(element, options) {
        this.$el = $(element);
                
        if (options) {
            $.extend( this, options );
        }
        
        /** PLUGIN CONSTANTS */
        this.VERSION = '2.2.2';

        /** PLUGIN METHODS */

        /**
        * SORT:
        * Arranges the data object in the order based on the object key
        * stored in the variable `sortBy` and the direction stored in the 
        * variable `reverse`.
        *
        * A date primer has been created. If the object value matches the 
        * date pattern, it be sorted as a date instead or a string or number.
        */
        this.sort = function() {
            var $this = this;
            var date = /^(Jan|January|Feb|February|Mar|March|Apr|April|May|Jun|June|Jul|July|Aug|August|Sep|September|Oct|October|Nov|November|Dec|December|(0?\d{1})|(10|11|12))(-|\s|\/|\.)(0?[1-9]|(1|2)[0-9]|3(0|1))(-|\s|\/|\.|,\s)(19|20)?\d\d$/i;
            
            function objectSort(field, reverse, primer){
                reverse = (reverse) ? -1 : 1;
                
                return function(a,b){
                
                    a = a[field];
                    b = b[field];
            
                    if (date.test(a) && date.test(b)) {
                        a = new Date(a);
                        a = Date.parse(a);
                        
                        b = new Date(b);
                        b = Date.parse(b);
                    } else if (typeof(primer) !== 'undefined'){
                        a = primer(a);
                        b = primer(b);
                    }
            
                    if (a<b) {
                        return reverse * -1;
                    }
                    
                    if (a>b) {
                        return reverse * 1;
                    }

                    return 0;
                };
            }
            
            if ($this.total && $this.sortBy && typeof $this.data[0][$this.sortBy] !== 'undefined') {
                $this.data.sort(objectSort($this.sortBy, $this.reverse));
            }
        };

        /**
        * FILTER: 
        * Filters out all row from the data object that does not match the 
        * the search query stored in the `query`. 
        * 
        * If the data object value is a string, the query can be anywhere in value
        * regardless of case. 
        *
        * If the data object value is a number, the query must match value only, not
        * data type. 
        */
        this.filter = function() {
            var $this = this,
            length = $this.searchableFields.length;

            if ($this.query) {
                var re = new RegExp($this.query, "gi");

                $this.data = $.grep($this.data, function(obj) {
                    for (var key = 0; key < length; key++) {
                        if (typeof obj[$this.searchableFields[key]] === 'string') {
                            if (obj[$this.searchableFields[key]].match(re)) {
                                return true;
                            }
                        } else if (typeof obj[$this.searchableFields[key]] === 'number') {
                            if (obj[$this.searchableFields[key]] == $this.query) {
                                return true;
                            }
                        }
                    }
                    return false;
                });
            }

            /** setting data total */ 
            $this.total = $this.data.length;
        };

        /**
        * PAGINATE: 
        * Calculates the number of pages, the current page number and the exact
        * rows to display. 
        */
        this.paginate = function() {
            var $this = this;

            /** calculate the number of pages */
            $this.pages = Math.ceil($this.data.length/$this.size);
            
            /** retrieve page number */
            $this.page = ($this.page <= $this.pages ? $this.page : 1);
            
            /** set range of rows */ 
            $this.setRange(); 
            
            $this.data = $this.data.slice($this.range.start-1,$this.range.end);

        };

        /**
        * CONDITION: 
        * Only displays the data object rows that meet the given criteria.
        * 
        * Condition vs Filter: 
        * Condition is true if the value meets a determined conditional statement, 
        * which is found in the schema. Condition is column specific. Since conditions 
        * are not subject to the end users actions, condition is only checked once during
        * initialization.
        *
        * Filter is true if the value matches the query. A query is compared across 
        * all searchable columns. Filter is checked every time there is a query value.
        *
        *
        */
        this.condition = function() {
            var $this = this,
            schema = [];

            if ($this.schema) {
                var dataLength = $this.data.length,
                schemaLength = $this.schema.length;

                for (var row = 0; row < dataLength; row++) {
                    var data = $this.data[row],
                    temp = {};
                    
                    for (var key = 0; key < schemaLength; key++) {
                        var val = $this.schema[key]; 

                        if(val.condition) {
                            if(!val.condition(data[val.key])) {
                                temp = null;
                                break;
                            }
                        }
                        
                        temp[val.key] = data[val.key];
                    }
                    
                    if (temp) {
                        schema.push(temp);
                    }
                }
                
                $this.data = schema;
            }
        };

        /**
        * CHEVRON
        * This a shortcut for compiling and render a Mustache template and data.
        */
        this.chevron = function(template, data) {
            return Mustache.render(template, data);
        };
        
        this.create = function() {
            var $this = this;

            //Building Data
            $this.resetData();

            if($this.searching) {
                $this.filter();
            }

            if($this.sorting) { 
                $this.sort();
            }
            
            if($this.paginating) {
                $this.paginate();
            }
            

            /** Building Column Elements */
            function buildThead() {
                $this.thead = [];
                
                $.each($this.schema, function(key, col) {
                    if (!col.hide) {
                        var th = {};
                        
                        if ($.inArray(col.key,$this.sortableFields) === -1) {
                            th.notSortable = true;
                        } else if ($this.sortBy === col.key) {
                            if ($this.reverse) {
                                th.sortedDown = true;
                            } else {
                                th.sortedUp = true;
                            }
                        } else {
                            th.sortable = true;
                        }
                        
                        th.key = col.key;
                        th.header = col.header;
                        
                        $this.thead.push(th);
                    }
                });
            }

            function buildRows(key, row) {
                var tr = [];
                
                if (key%2 === 0) {
                    tr.push('<tr data-columns-row-id="'+key+'" class="'+$this.evenRowClass+'">');
                } else {
                    tr.push('<tr data-columns-row-id="'+key+'" class="'+$this.oddRowClass+'">');
                }
                 
                $.each($this.schema, function(key, col) {
                    if (!col.hide) {
                        if (col.template) {
                            tr.push('<td>'+$this.chevron(col.template, row)+'</td>');
                        } else {
                            tr.push('<td>'+row[col.key]+'</td>');
                        }
                    }
                });
                
                tr.push('</tr>');
                
                return tr;
            }

            function buildShowRowsMenu() {
                var menu = [];
                
                menu.push('<select>');
                
                $.each($this.showRows, function(key, val) {
                    var option = '<option value="'+val+'"';
                    
                    if(val === $this.size) {
                        option += 'selected="selected"';
                    }
                    
                    option += '>'+val+'</option>';
                    
                    menu.push(option);
                });
                
                menu.push('</select>');

                $this.showRowsMenu = menu.join('');
            }
            
            function buildTable() {
                $this.rows = [];

                if($this.total) {
                    $.each($this.data, function(key, row) {
                        if (key === 0) {
                            buildThead();
                        }
                        $this.rows.push(buildRows(key, row).join(''));
                    });
                } else { 
                    $this.rows.push('<tr class="'+$this.evenRowClass+'"><td colspan="'+$this.schema.length+'"><em>Nenhum resultado encontrado</em></td>');

                }
            }
            
            buildTable();
            buildShowRowsMenu();
            
            /** Creating Table from Mustache Template */
            var view = {
                prevPage: $this.page-1,
                nextPage: $this.page+1,
                prevPageExists: $this.pageExists($this.page-1),
                nextPageExists: $this.pageExists($this.page+1),
                resultRange: $this.range,
                tableTotal: $this.total,
                showRowsMenu: $this.showRowsMenu,
                rows: $this.rows,
                headers: $this.thead,
                query: $this.query,
                search: $this.search,
                table: $this.table
            };

            $.extend($this.view, view);

            /** Calling plugins, if any */
            if ($this.plugins) {
                $.each($this.plugins, function(key, val) {
                    if (typeof ColumnsPlugins !== 'undefined') {
                        if (typeof ColumnsPlugins[val] !== 'undefined') {
                            ColumnsPlugins[val].create.call($this);
                        }
                    }
                });
            }

            if ($this.search) {
                $this.$el.html($this.chevron($this.template, $this.view));
                $this.search = false;
            } else {
                $('[data-columns-table]', $this.$el).remove();
                $this.$el.append($this.chevron($this.template, $this.view));
            }

            return true;
        };
        
        this.init = function() {
            var $this = this;

            function buildSchema() {
                $this.schema = [];
                $.each($this.data[0], function(key) {
                    $this.schema.push({"header":key, "key":key});
                });
            }
            
            function buildSearchableFields() {
                $this.searchableFields = [];
                $.each($this.data[0], function(key) {
                    $this.searchableFields.push(key);
                });
            }
            
            function buildSortableFields() {
                $this.sortableFields = [];
                $.each($this.data[0], function(key) {
                    $this.sortableFields.push(key);
                });
            }
            
            function getTemplateFile() {
                $.ajax({
                    url: $this.templateFile,
                    async: false,
                    success: function(template) {
                        $this.template = template;
                    },
                    error: function() {
                        $.error('Template could not be found.');
                    }
                });
            }
            
            if ($.isArray($this.data)) {
                $this.master = [];
                $this.view = {};
                
                /** setting up DOM */
                $this.$el.addClass('columns');

                /** creating listeners */

                /** sort listener */
                $this.$el.on('click', '.ui-table-sortable', function(event) {
                    var sortBy = $(this).data('columns-sortby');
                    
                    if ($this.sortBy === sortBy) {
                        $this.reverse = ($this.reverse) ? false : true;
                    }

                    $this.sortBy = sortBy

                    $this.sortHandler(event);
                });

                /** page listener */
                $this.$el.on('click', '.ui-table-control-next, .ui-table-control-prev', function(event) {
                    $this.page = $(this).data('columns-page');
                    
                    $this.pageHandler(event);
                });

                /** search listener */
                $this.$el.on('keyup', '.ui-table-search', function(event) {
                    $this.query = $(this).val();
                    
                    $this.searchHandler(event); 
                });

                /** size listener */
                $this.$el.on('change', '.ui-table-size select', function(event) {
                    $this.size = parseInt($(this).val());
                    
                    $this.sizeHandler(event);
                });

                /** Calling plugins, if any */
                if ($this.plugins) {
                    $.each($this.plugins, function(key, val) {
                        if (typeof ColumnsPlugins !== 'undefined') {
                            if (typeof ColumnsPlugins[val] !== 'undefined') {
                                ColumnsPlugins[val].init.call($this);
                            }
                        }
                    });
                }

                /** condition never change, so only checked once. */
                if($this.conditioning) {
                    $this.condition();
                }

                /** updating defaults */
                if (!$this.schema) {
                    buildSchema();
                }
                
                if (!$this.searchableFields) {
                    buildSearchableFields();
                }
                
                if (!$this.sortableFields) {
                    buildSortableFields();
                }
                
                if ($this.templateFile) {
                    getTemplateFile();
                }

                /** making a master copy of data */
                $.extend($this.master, $this.data);

                /** creating columns table */
                $this.create();

            } else {
                $.error('The "data" parameter must be an array.');
            }
        
        };
        
        this.init();
    };

    Columns.prototype = {

        //defaults
        evenRowClass: "ui-table-rows-even",
        oddRowClass: "ui-table-rows-odd",
        liveSearch: true,
        page: 1,
        pages: 1,
        plugins: null,
        query: null,
        reverse: false,
        pagination: true,
        schema: null,
        search: true,
        searchableFields: null,
        showRows: [5, 10, 25, 50],
        size: 5,
        sortableFields: null,
        sortBy: null,
        table: true,
        templateFile: null,
        template: '{{#table}} <div data-columns-table="true"> <div class="table-responsive"> <table class="table table-striped table-condensed"> <thead class="bg-primary"> {{#headers}} {{#sortable}} <th class="ui-table-sortable" data-columns-sortby="{{key}}">{{header}}</th> {{/sortable}} {{#notSortable}} <th class="not-sortable">{{header}}</th> {{/notSortable}} {{#sortedUp}} <th class="ui-table-sortable" data-columns-sortby="{{key}}">{{header}} <span>&#x25B2;</span></th> {{/sortedUp}} {{#sortedDown}} <th class="ui-table-sortable" data-columns-sortby="{{key}}">{{header}} <span>&#x25BC;</span></th> {{/sortedDown}} {{/headers}} </thead> <tbody> {{#rows}} {{{.}}} {{/rows}} </tbody> </table> </div>  {{#pagesTotal}} <nav class="text-center"> <ul class="pagination"> <li><span class="glyphicon">{{actualPage}} de {{pagesTotal}}</span></li> {{#prevPageExists}} <li><span class="ui-table-control-next glyphicon glyphicon-step-backward btn btn-link" data-columns-page="1"></span></li> <li><span class="ui-table-control-next glyphicon glyphicon-chevron-left btn btn-link" data-columns-page="{{prevPage}}"></span></li> {{/prevPageExists}} {{^prevPageExists}} <li class="disabled"><span class="glyphicon glyphicon-step-backward btn btn-link"></span></li> <li class="disabled"><span class="glyphicon glyphicon-chevron-left btn btn-link"></span></li> {{/prevPageExists}} {{#indexes}} {{#value}} <li class="active"><span class="ui-table-control-next glyphicon btn btn-link" data-columns-page="{{key}}">{{key}}</span></li> {{/value}} {{^value}} <li><span class="ui-table-control-next glyphicon btn btn-link" data-columns-page="{{key}}">{{key}}</span></li> {{/value}} {{/indexes}} {{#nextPageExists}} <li><span class="ui-table-control-next glyphicon glyphicon-chevron-right btn btn-link" data-columns-page="{{nextPage}}"></span></li> <li><span class="ui-table-control-next glyphicon glyphicon-step-forward btn btn-link" data-columns-page="{{pagesTotal}}"></span></li> {{/nextPageExists}} {{^nextPageExists}} <li class="disabled"><span class="glyphicon glyphicon-chevron-right btn btn-link"></span></li> <li class="disabled"><span class="glyphicon glyphicon-step-forward btn btn-link"></span></li> {{/nextPageExists}} </ul> </nav> {{/pagesTotal}} </div> {{/table}}',

        //functionality
        conditioning: true,
        paginating: true,
        searching: true,
        sorting: true,


        //Handlers
        pageHandler: function() {
            this.create();
        },
        searchHandler: function(event) { 
            if(this.liveSearch) {
                this.create();
            } else {
                if(event.keyCode == '13') {
                    this.create();
                }
            }
        },
        sizeHandler: function() {
            this.create();
        },
        sortHandler: function() {
            this.page = 1;
            this.create();
        },

        //API
        destroy: function() {
            this.$el.data('columns', null);
            this.$el.empty();
            this.$el.unbind();
            return true;
        }, 
        getObject: function() {
            return this;
        },
        getPage: function() {
            return this.page;
        },
        getQuery: function() {
            return this.query;
        },
        getRange: function() {
            return this.range;
        },
        getRows: function() {
            return this.rows;
        },
        getShowRowsMenu: function() {
            return this.showRowsMenu;
        },
        getTemplate: function() {
            return this.template;
        },
        getThead: function() {
            return this.thead;
        },
        getTotal: function() {
            return this.total;
        },
        getVersion: function() {
            return this.VERSION;
        },
        getView: function() {
            return this.view;
        },
        gotoPage: function (p) {
            alert("gotoPage: function (p) {");
            if(this.pageExists(p)) {
                this.page = p;
                this.create();
                return true;
            }
            
            return false;
        },
        pageExists: function(p) {
            return (p > 0 && p <= this.pages) ? true : false;
        },
        resetData: function(d) {
            this.data = this.master.slice(0);
            return this.data;
        },
        setMaster: function(d) {
            if ($.isArray(d)) {
                this.master = d;
                return true;
            } 

            return false;
        },
        setPage: function(p) { 
            this.page = (this.pageExists(p) ? p : this.page); 
            return this.page; 
        },
        setRange: function() { 
            var start = ((this.page -1) * (this.size));
            var end = (start + this.size < this.total) ? start + this.size : this.total;
        
            this.range = {"start":start+1, "end":end};
        },
        setTotal: function(t) {
            this.total = t;

            return true;
        },

        //performance tracking
        startTime: null, 
        endTime: null,
        startTimer: function() { 
            var now = new Date();
            this.startTime =  now.getTime(); 
        }, 
        endTimer: function() {
            var now = new Date(); 
            this.endTime =  now.getTime();
        },
        getTimer: function() { 
            console.log((this.endTime - this.startTime)/1000);
        }
    };
    
})(jQuery);