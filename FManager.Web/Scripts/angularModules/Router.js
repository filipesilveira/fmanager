angular.module('FManagerApp').config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when("/", {
            title: 'Bancas',
            templateUrl: "/Accounts/AccountsList"
        })
        .when("/accounts", {
            title: 'Bancas',
            templateUrl: "/Accounts/AccountsList"
        })
        .when("/accounts/:accountId", {
            title: 'Bancas',
            templateUrl: "/Accounts/AccountsView"
        })
        .when("/accounts/:accountId/:sectionId", {
            title: 'Bancas',
            templateUrl: "/Sections/SectionsView"
        })
        .when("/daybooks", {
            title: 'Diários',
            templateUrl: "/DayBooks/DayBooksList"
        })
        .when("/daybooks/:id", {
            title: 'Diários',
            templateUrl: "/DayBooks/DayBooksView"
        })
        .when("/assertiveness", {
            title: 'Assertividade',
            templateUrl: "/Assertiveness/AssertivenessList"
        })
        .when("/reviews", {
            title: 'Resumos',
            templateUrl: "/Reviews/ReviewsList"
        })
        .when("/tradinghistory", {
            title: 'Histórico',
            templateUrl: "/TradingHistory/TradingHistoryList"
        })
        .when("/soros", {
            title: 'Gerenciamento Soros',
            templateUrl: "/Soros/SorosList"
        })
        .otherwise({ redirectTo: '/' });
}]);