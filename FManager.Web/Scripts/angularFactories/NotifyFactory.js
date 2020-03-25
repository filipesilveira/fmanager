angular.module("FManagerApp").factory('NotifyFactory', ['$http', function ($http) {
    var service = {};

    service.success = function () {
        $.notify('Success', {
            delay: 2000,
            placement: {
                from: 'bottom',
                align: 'left'
            },
            template: `
                <div class="col-xs-11 col-sm-2 alert alert-success alert-dismissible fade show" role="alert">
                  Salvo com sucesso.
                  <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                      <i class="tim-icons icon-simple-remove"></i>
                  </button>
                </div>
            `
        });
    };

    service.error = function () {
        $.notify('Error', {
            delay: 2000,
            placement: {
                from: 'bottom',
                align: 'left'
            },
            template: `
                <div class="col-xs-11 col-sm-2 alert alert-danger alert-dismissible fade show" role="alert">
                  Erro, por favor tente novamente mais tarde.
                  <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                      <i class="tim-icons icon-simple-remove"></i>
                  </button>
                </div>
            `
        });
    };

    service.warning = function (message) {
        $.notify('Warning', {
            delay: 2000,
            placement: {
                from: 'bottom',
                align: 'left'
            },
            template: `
                <div class="col-xs-11 col-sm-2 alert alert-warning alert-dismissible fade show" role="alert">
                  ${message}
                  <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                      <i class="tim-icons icon-simple-remove"></i>
                  </button>
                </div>
            `
        });
    };

    return service;
}]);