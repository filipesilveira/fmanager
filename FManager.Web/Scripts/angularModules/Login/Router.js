angular.module('FManagerApp').config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when("/", {
            title: 'Login',
            templateUrl: "/Login/LoginView"
        })
        .when("/newuser", {
            title: 'Novo usuário',
            templateUrl: "/Users/UsersEdit"
        })
        .when("/payment", {
            title: 'Pagamento',
            templateUrl: "/Users/UsersPayment"
        })
        .otherwise({ redirectTo: '/' });
}]);