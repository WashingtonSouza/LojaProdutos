//https://github.com/jvitela/mustache-wax

if (!String.prototype.trim) {
    String.prototype.trim = function () {
        return this.replace(/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g, '');
    };
}

(function (root, wax) {
    // Set up Backbone appropriately for the environment. Start with AMD.
    if (typeof define === 'function' && define.amd) {
        define(['mustache'], function (mustache) {
            wax(mustache);
        });

        // Next for Node.js or CommonJS. jQuery may not be needed as a module.
    } else if (typeof exports !== 'undefined') {
        var mustache = require('mustache');
        wax(mustache);

        // Finally, as a browser global.
    } else {
        wax(root.Mustache);
    }
}(this, function Wax(Mustache) {
    Mustache.Formatters = {};

    /*
     *	This will parse a parameter from a filter:
     *  
     *      {{ vaue | filter : param1 : param2 : param3 }}
     */
    Mustache.Context.prototype.parseParam = function parseParam(param) {
        var isString, isInteger, isFloat;
        isString = /^[\'\"](.*)[\'\"]$/g;
        isInteger = /^[+-]?\d+$/g;
        isFloat = /^[+-]?\d*\.\d+$/g;
        if (isString.test(param)) {
            return param.replace(isString, '$1');
        }
        if (isInteger.test(param)) {
            return parseInt(param, 10);
        }
        if (isFloat.test(param)) {
            return parseFloat(param);
        }
        return this._lookup(param);
    };

    /*
     *	This function will resolve one filter# in the mustache expression:
     *  
     *      {{ value | filter1 | filter2 | ... | filterN }}
     */
    Mustache.Context.prototype.applyFilter = function applyFilter(expr, fltr) {
        var filterExp, paramsExp, match, filter, params = [expr];
        filterExp = /^\s*([^\:]+)/g;
        paramsExp = /\:\s*([\'][^\']*[\']|[\"][^\"]*[\"]|[^\:]+)\s*/g;
        match = filterExp.exec(fltr);
        filter = match[1].trim();
        while ((match = paramsExp.exec(fltr))) {
            params.push(this.parseParam(match[1].trim()));
        }
        //console.log( filter);
        //console.log( params);
        if (Mustache.Formatters.hasOwnProperty(filter)) {
            fltr = Mustache.Formatters[filter];
            return fltr.apply(fltr, params);
        }
        return expr;
    };

    /*
     * Keep a copy of the original lookup function of Mustache
     */
    Mustache.Context.prototype._lookup = Mustache.Context.prototype.lookup;

    /*
     * Overwrite the Context::lookup method to add filter capabilities
     */
    Mustache.Context.prototype.lookup = function parseExpression(name) {
        var i, l, expression, formatters;
        formatters = name.split("|");
        expression = formatters.shift().trim();
        //console.log(expression);
        expression = this._lookup(expression);
        for (i = 0, l = formatters.length; i < l; ++i) {
            expression = this.applyFilter(expression, formatters[i]);
        }
        return expression;
    };
}));

Mustache.Formatters = {
    "number": function (value, decimals) {
        if (typeof value === "number")
            return Globalize.format(value, 'N' + decimals);
        else
            return value;
    },
    "percent": function (value, decimals) {
        if (typeof value === "number")
            return Globalize.format(value, 'N' + decimals) + '%';
        else
            return value;
    },
    "date": function (value, format) {
        if (value) {
            if (value instanceof Date)
                return Globalize.format(value, format);
            else
                return Globalize.format(new Date(value), format);
        }
    },
    "zipcode": function (value) {
        var onlynumbers = Mustache.Formatters.onlynumbers;
        var mask = Mustache.Formatters.mask;
        return mask(onlynumbers(value), '00000-000', '0', 8);
    },
    "cnpj": function (value) {
        var onlynumbers = Mustache.Formatters.onlynumbers;
        var mask = Mustache.Formatters.mask;
        return mask(onlynumbers(value), '00.000.000/0000-00', '0', 14);
    },
    "cpf": function (value) {
        var onlynumbers = Mustache.Formatters.onlynumbers;
        var mask = Mustache.Formatters.mask;
        return mask(onlynumbers(value), '000.000.000-00', '0', 11);
    },
    "ip": function (value) {
        return value;
    },
    "phone": function (value) {
        var onlynumbers = Mustache.Formatters.onlynumbers;
        var mask = Mustache.Formatters.mask;
        return mask(onlynumbers(value), '(00) 0000-0000', ' ', 10);
    },
    "mask": function (value, format, fill, length) {
        var lpad = Mustache.Formatters.lpad;
        return $('<div>').text(lpad(value, length, fill)).mask(format).text();
    },
    "lpad": function (value, length, sep) {
        sep = sep || " ";
        value = "" + value;
        var filler = "";
        while ((filler.length + value.length) < length) { filler += sep };
        return (filler + value).slice(-length);
    },
    "onlynumbers": function (value) {
        var str = value + '';
        return str.replace(/[^0-9]/g, '');
    }
};