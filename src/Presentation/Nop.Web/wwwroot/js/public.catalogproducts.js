var CatalogProducts = {
  settings: {
    ajax: false,
    fetchUrl: false,
    browserPath: false,

    //todo: add setting to get products manually (e.g. after user press on submit button). Exclude paging.
  },

  params: {
    jqXHR: false,
  },

  init: function (settings) {
    this.settings = $.extend({}, this.settings, settings);

    var self = this;

    var $viewModeEls = $('[data-viewmode]');
    if ($viewModeEls) {
      $viewModeEls.on('click', function () {
        if (!$(this).hasClass('selected')) {
          $viewModeEls.toggleClass('selected');
          self.getProducts();
        }
        return false;
      });
    }

    var $orderByEl = $('#products-orderby');
    if ($orderByEl) {
      $orderByEl
        .attr('onchange', null).off('change')
        .on('change', function () {
        self.getProducts();
      });
    }

    var $pageSizeEl = $('#products-pagesize');
    if ($pageSizeEl) {
      $pageSizeEl
        .attr('onchange', null).off('change')
        .on('change', function () {
        self.getProducts();
      });
    }

    var $optionEls = $('[data-option-id]');
    if ($optionEls) {
      $optionEls.on('change', function () {
        self.getProducts();
      });
    }

    var $msEls = $('[data-manufacturer-id]');
    if ($msEls) {
      $msEls.on('change', function () {
        self.getProducts();
      });
    }

    this.addPagingHandlers();
  },

  getProducts: function (pageNumber) {
    if (this.params.jqXHR && this.params.jqXHR.readyState !== 4) {
      this.params.jqXHR.abort();
    }

    var urlBuilder = createProductsURLBuilder(this.settings.browserPath);

    var $optionEls = $('[data-option-id]');
    if ($optionEls) {
      var selectedOptions = $.map($optionEls, function (el) {
        var $optionEl = $(el);
        if ($optionEl.is(':checked')) return $(el).data('option-id')
        return null;
      });

      if (selectedOptions && selectedOptions.length > 0) {
        urlBuilder.addOptions(selectedOptions);
      }
    }

    var $msEls = $('[data-manufacturer-id]');
    if ($msEls) {
      var selectedMs = $.map($msEls, function (el) {
        var $mEl = $(el);
        if ($mEl.is(':checked')) return $(el).data('manufacturer-id')
        return null;
      });

      if (selectedMs && selectedMs.length > 0) {
        urlBuilder.addManufacturers(selectedMs);
      }
    }

    var $viewModeEl = $('[data-viewmode].selected');
    if ($viewModeEl) {
      urlBuilder.addViewMode($viewModeEl.data('viewmode'));
    }

    var $pageSizeEl = $('#products-pagesize');
    if ($pageSizeEl) {
      urlBuilder.addPageSize($pageSizeEl.val());
    }

    var $orderEl = $('#products-orderby');
    if ($orderEl) {
      urlBuilder.addOrder($orderEl.val());
    }

    var $priceRangeEl = $('#price-range-slider');
    if ($priceRangeEl) {
      var priceRange = $priceRangeEl.slider('values');
      if (priceRange && priceRange.length > 0) {
        urlBuilder.addPrices(priceRange);
      }
    }

    if (pageNumber) {
      urlBuilder.addPageNumber(pageNumber);
    }

    var beforePayload = {
      urlBuilder
    };
    $(this).trigger({ type: "before", payload: beforePayload });

    this.setBrowserHistory(urlBuilder.build());

    if (!this.settings.ajax) {
      setLocation(urlBuilder.build());
    } else {
      this.setLoadWaiting(1);

      urlBuilder.addBaseUrl(this.settings.fetchUrl);

      var self = this;
      this.params.jqXHR = $.ajax({
        cache: false,
        url: urlBuilder.build(),
        type: 'GET',
        success: function (response) {
          var $wrapper = $('.products-wrapper');
          if ($wrapper) {
            $wrapper.html(response);
            self.addPagingHandlers();
          }

          // todo: fire event on success
        },
        error: function (jqXHR, textStatus, errorThrown) {

        },
        complete: function (jqXHR, textStatus) {
          self.setLoadWaiting();
        }
      });
    }
  },

  addPagingHandlers: function () {
    var self = this;
    var $pageEls = $('[data-page]');
    if ($pageEls) {
      $.each($pageEls, function (i, el) {
        var $el = $(el);
        if ($el.is('a')) $el.removeAttr('href');
      });
      $pageEls.on('click', function () {
        self.getProducts($(this).data('page'));
      });
    }
  },

  setLoadWaiting(enable) {
    var $busyEl = $('.ajax-products-busy');
    if (enable) {
      $busyEl.show();
    } else {
      $busyEl.hide();
    }
  },

  setBrowserHistory(url) {
    // q: 'back to' link should get products ?
    window.history.pushState({ path: url }, '', url);
  }
}

function createProductsURLBuilder(baseUrl) {
  return {
    params: {
      baseUrl: baseUrl,
      query: {}
    },

    addBaseUrl: function (url) {
      this.params.baseUrl = url;
      return this;
    },

    addPrices: function (range) {
      this.params.query.price = range.join('-');
      return this;
    },

    addManufacturers: function (manufacturers) {
      this.params.query.ms = manufacturers.join(',');
      return this;
    },

    addOptions: function (options) {
      this.params.query.specs = options.join(',');
      return this;
    },

    addPageSize: function (pageSize) {
      this.params.query.pagesize = pageSize;
      return this;
    },

    addPageNumber: function (pageNumber) {
      this.params.query.pagenumber = pageNumber;
      return this;
    },

    addOrder: function (order) {
      this.params.query.orderby = order;
      return this;
    },

    addViewMode: function (viewMode) {
      this.params.query.viewmode = viewMode;
      return this;
    },

    addCustomParameter: function (name, value) {
      this.params.query[name] = value;
      return this;
    },

    build: function () {
      var query = $.param(this.params.query);
      var url = this.params.baseUrl;

      return url.indexOf('?') !== -1
        ? url + '&' + query
        : url + '?' + query;
    }
  }
}