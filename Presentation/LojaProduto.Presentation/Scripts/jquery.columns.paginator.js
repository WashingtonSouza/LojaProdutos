if (typeof ColumnsPlugins === 'undefined') var ColumnsPlugins = {};

ColumnsPlugins.paginator = {
    init: function () {
        var $this = this;

        /** turning off default functionality */
        $this.conditioning = false;
        $this.paginating = false;
        $this.searching = false;
        $this.searchableFields = [];
        $this.sorting = false;

        /** creating default handler */
        var handler = function () {

            var hasFilters = typeof (getFilters) == 'function' ? true : false;

            $.ajax({
                type: 'POST',
                url: $this.url,
                dataType: 'json',
                cache: false,
                async: true,
                data: $.extend({
                    "startIndex": ($this.page - 1) * $this.size,
                    "pageSize": $this.size,
                    "orderProperty": $this.sortBy,
                    "orderAscending": !$this.reverse
                }, hasFilters ? getFilters() : {}),
                error: function (xhr) {
                    showMessage(xhr.responseJSON);
                },
                success: function (json) {
                    $this.total = json.Entities.length;
                    $this.pages = Math.ceil(json.RowsCount / json.PageSize);
                    $this.rowsCount = json.RowsCount;
                    $this.startIndex = json.StartIndex;
                    $this.pageSize = json.PageSize;
                    $this.actualPage = Math.trunc(json.StartIndex / json.PageSize) + 1;
                    $this.setMaster(json.Entities);
                    $this.create();

                    $this.$el.find('a.confirm-delete').click(function (event) {

                        event.preventDefault();

                        showConfirmDelete($(this), $this.$el.find('ul.pagination li.active span'));

                        return false;
                    });
                }
            });
        }

        /** override handlers */
        $this.pageHandler = handler;
        $this.sizeHandler = handler;

        /** search handler, sets page to 1 first */
        $this.searchHandler = function () {
            $this.page = 1;
            handler();
        }

        /** sort handler, sets page to 1 first */
        $this.sortHandler = function () {
            handler();
        }
    },

    create: function () {
        var $this = this;

        /** setting current result range */
        $this.setRange();

        /** setting paginator variables */
        $this.pages = Math.ceil($this.rowsCount / $this.pageSize);
        $this.actualPage = Math.trunc($this.startIndex / $this.pageSize) + 1;

        var pageRadius = 10;
        var startPage = ($this.actualPage - pageRadius / 2);
        if ((startPage + pageRadius - 1) > $this.pages)
            startPage = $this.pages - pageRadius + 1;
        startPage = (startPage < 1 ? 1 : startPage);
        var endPage = (startPage + pageRadius - 1);
        if (endPage > $this.pages)
            endPage = $this.pages;

        var indexes = [];
        for (var i = startPage; i <= endPage; i++)
            indexes.push({ key: i, value: (i == $this.actualPage) });

        /** setting view variables */
        $this.view.indexes = indexes;
        $this.view.rowsCount = $this.rowsCount;
        $this.view.pageSize = $this.pageSize;
        $this.view.startIndex = $this.startIndex;
        $this.view.actualPage = $this.actualPage;
        $this.view.tableTotal = $this.total;
        $this.view.pagesTotal = $this.pages;
        $this.view.prevPageExists = $this.pageExists($this.page - 1);
        $this.view.nextPageExists = $this.pageExists($this.page + 1);
        $this.view.resultRange = $this.range;
    }
}