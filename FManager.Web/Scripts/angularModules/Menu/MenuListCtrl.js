angular.module('FManagerApp').controller('MenuListCtrl', ['$scope', '$rootScope', '$route', 'UsersService',
    function ($scope, $rootScope, $route, UsersService) {

        $rootScope.$on('$routeChangeStart', function (event, next) {
            $scope.route = next.$$route.title;
            $scope.showLoadingIndicator = true;
        });

        $rootScope.$on('$routeChangeSuccess', function () {
            $scope.pageTitle = $route.current.title;
            $scope.showLoadingIndicator = false;
            $('.ps').scrollTop(0);
        });

        UsersService.getCurrentUserName().then(function (response) {
            $scope.currentUserName = response.data;
        });

        $scope.menuData = [
            { Name: "Bancas", Url: "/#/", Icon: "tim-icons icon-app" },
            { Name: "Histórico", Url: "/#/tradinghistory", Icon: "tim-icons icon-refresh-01" },
            { Name: "Assertividade", Url: "/#/assertiveness", Icon: "tim-icons icon-check-2" },
            { Name: "Resumos", Url: "/#/reviews", Icon: "tim-icons icon-notes" },
            { Name: "Gerenciamento Soros", Url: "/#/soros", Icon: "tim-icons icon-spaceship" },
            { Name: "Diários", Url: "/#/daybooks", Icon: "tim-icons icon-book-bookmark" },
            { Name: "Sair", Url: "/Login/Logout", Icon: "tim-icons icon-minimal-left" }
        ];
    }]);