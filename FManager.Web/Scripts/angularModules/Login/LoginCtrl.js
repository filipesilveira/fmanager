(function () {
    'use strict';

    angular
        .module('FManagerApp')
        .controller('LoginCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$rootScope', 'LoginService', 'NotifyFactory', '$window', '$location'];

    function LoginCtrl($rootScope, LoginService, NotifyFactory, $window, $location) {
        var vm = this;

        vm.login = login;

        function login() {
            vm.showLoadingIndicator = true;

            LoginService.post(vm.LoginData)
                .then(function (response) {
                    if (!response.data.Payment) {
                        $location.path('/payment');
                    }
                    else if (response.data.Login) {
                        $window.location = "/#";
                    }
                    else {
                        NotifyFactory.error();
                        vm.showLoadingIndicator = false;
                    }
                })
                .catch(function () {
                    NotifyFactory.error();
                    vm.showLoadingIndicator = false;
                });
        }

        $rootScope.$on('$routeChangeStart', function () {
            vm.showLoadingIndicator = true;
        });

        $rootScope.$on('$routeChangeSuccess', function () {
            vm.showLoadingIndicator = false;
        });
    }
})();