(function () {
    'use strict';

    angular
        .module("FManagerApp")
        .service("UsersService", UsersService);

    UsersService.$inject = ['HttpFactory'];

    function UsersService(HttpFactory) {
        var vm = this;

        vm.createPaymentUrl = (paymentPlanId) => HttpFactory.get(`/api/Payments/CreatePaymentUrl?paymentPlanId=${paymentPlanId}`);
        vm.getCurrentUserName = () => HttpFactory.get("api/Users/GetCurrentUserName");
        vm.getPaymentUrl = () => HttpFactory.get("api/Payments/GetPaymentUrl");
        vm.getPaymentPlans = () => HttpFactory.get("api/Users/GetPaymentPlans");
        vm.post = (data) => HttpFactory.post("api/Users", data);
    }
})();