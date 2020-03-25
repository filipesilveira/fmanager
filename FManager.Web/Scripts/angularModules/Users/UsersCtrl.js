(function () {
    'use strict';

    angular
        .module('FManagerApp')
        .controller('UsersCtrl', UsersCtrl);

    UsersCtrl.$inject = ['UsersService', 'NotifyFactory', '$window', '$location'];

    function UsersCtrl(UsersService, NotifyFactory, $window, $location) {
        var vm = this;

        vm.save = save;
        vm.payment = payment;

        initialize();

        function initialize() {
            UsersService.getPaymentUrl()
                .then(function (response) {
                    vm.paymentUrl = response.data;
                });

            UsersService.getPaymentPlans()
                .then(function (response) {
                    vm.paymentPlans = response.data;
                });
        }

        function save() {
            vm.showLoadingIndicator = true;

            UsersService.post(vm.UserData)
                .then(function (response) {
                    if (response.data) {
                        $location.path('/payment');
                    }
                    else {
                        NotifyFactory.warning('Já existe uma conta com esse e-mail, por favor tente novamente.');
                    }
                    vm.showLoadingIndicator = false;
                })
                .catch(function () {
                    NotifyFactory.error();
                    vm.showLoadingIndicator = false;
                });
        }

        function payment() {
            vm.showLoadingIndicator = true;

            UsersService.createPaymentUrl(vm.PaymentData.PaymentPlanId)
                .then(function (response) {
                    $window.location = response.data;
                })
                .catch(function () {
                    NotifyFactory.error();
                    vm.showLoadingIndicator = false;
                });
        }
    }
})();