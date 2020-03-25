using System.Web;
using System.Web.Optimization;

namespace FManager
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var css = new StyleBundle("~/Content/css")
                .Include("~/BlackDashboard/assets/css/nucleo-icons.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/lib/glyphicons/css/glyphicons.css", new CssRewriteUrlTransform())
                .Include("~/BlackDashboard/assets/css/black-dashboard.css", new CssRewriteUrlTransform())
                .Include("~/BlackDashboard/assets/demo/demo.css", new CssRewriteUrlTransform())
                .Include("~/Content/site.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-datetimepicker.css", new CssRewriteUrlTransform());

            var baseScripts = new ScriptBundle("~/bundles/BaseScripts")
                //Template files
                .Include("~/BlackDashboard/assets/js/core/jquery.min.js")
                .Include("~/BlackDashboard/assets/js/core/popper.min.js")
                .Include("~/BlackDashboard/assets/js/core/bootstrap.min.js")
                .Include("~/BlackDashboard/assets/js/plugins/perfect-scrollbar.jquery.min.js")
                .Include("~/BlackDashboard/assets/js/plugins/chartjs.min.js")
                .Include("~/BlackDashboard/assets/js/plugins/bootstrap-notify.js")
                .Include("~/BlackDashboard/assets/js/black-dashboard.min.js")
                .Include("~/BlackDashboard/assets/demo/demo.js")
                .Include("~/Scripts/lib/templateControls.js")
                .Include("~/Scripts/lib/pageLoading.js")

                //Libs
                .Include("~/Scripts/lib/angular.js")
                .Include("~/Scripts/lib/angular-route.js")
                .Include("~/Scripts/lib/ui-bootstrap-tpls-1.2.4.js")
                .Include("~/Scripts/lib/moment-with-locales.js")
                .Include("~/Scripts/lib/bootstrap-datetimepicker.js")
                .Include("~/Scripts/lib/notify.min.js")
                .Include("~/Scripts/lib/clipboard.min.js")
                .Include("~/Scripts/lib/ngclipboard.min.js")
                .Include("~/Scripts/lib/ng-table.min.js")

                //Angular config
                .Include("~/Scripts/angularModules/FManagerApp.js")

                //Angular Filters
                .Include("~/Scripts/angularFilters/SafeHtmlFilter.js")

                //Angular Factories
                .Include("~/Scripts/angularFactories/HttpFactory.js")
                .Include("~/Scripts/angularFactories/NotifyFactory.js")
                .Include("~/Scripts/angularFactories/CsvFactory.js")

                //Angular directives
                .Include("~/Scripts/angularDirectives/DatePicker.js")
                .Include("~/Scripts/angularDirectives/CsvFileReader.js")
                .Include("~/Scripts/angularModules/Common/LoadingIndicator/LoadingIndicatorDirective.js");

            var loginScripts = new ScriptBundle("~/bundles/LoginScripts")
               .Include("~/Scripts/angularModules/Login/Router.js")

               //Angular Modules
               .Include("~/Scripts/angularModules/Login/LoginService.js")
               .Include("~/Scripts/angularModules/Login/LoginCtrl.js")
               .Include("~/Scripts/angularModules/Login/LoginRouteCtrl.js")

               .Include("~/Scripts/angularModules/Users/UsersService.js")
               .Include("~/Scripts/angularModules/Users/UsersCtrl.js");

            var applicationScripts = new ScriptBundle("~/bundles/ApplicationScripts")
                .Include("~/Scripts/angularModules/Router.js")

                //Angular Modules
                .Include("~/Scripts/angularModules/Menu/MenuListCtrl.js")
                .Include("~/Scripts/angularModules/Menu/MenuService.js")
                .Include("~/Scripts/angularModules/Users/UsersService.js")
                .Include("~/Scripts/angularModules/ManageBO/Currencies/CurrenciesService.js")
                .Include("~/Scripts/angularModules/ManageBO/Parities/ParitiesService.js")
                .Include("~/Scripts/angularModules/ManageBO/Accounts/AccountsListCtrl.js")
                .Include("~/Scripts/angularModules/ManageBO/Accounts/AccountsService.js")
                .Include("~/Scripts/angularModules/ManageBO/Accounts/AccountsViewCtrl.js")
                .Include("~/Scripts/angularModules/ManageBO/Accounts/AccountsCtrl.js")
                .Include("~/Scripts/angularModules/ManageBO/Sections/SectionsService.js")
                .Include("~/Scripts/angularModules/ManageBO/Sections/SectionsViewCtrl.js")
                .Include("~/Scripts/angularModules/ManageBO/Entries/EntriesService.js")
                .Include("~/Scripts/angularModules/ManageBO/Entries/EntriesCtrl.js")
                .Include("~/Scripts/angularModules/ManageBO/Assertiveness/AssertivenessService.js")
                .Include("~/Scripts/angularModules/ManageBO/Assertiveness/AssertivenessCtrl.js")
                .Include("~/Scripts/angularModules/ManageBO/TradingHistory/TradingHistoryService.js")
                .Include("~/Scripts/angularModules/ManageBO/TradingHistory/TradingHistoryCtrl.js")
                .Include("~/Scripts/angularModules/ManageBO/DaysOfWeek/DaysOfWeekService.js")
                .Include("~/Scripts/angularModules/ManageBO/Reviews/ReviewsService.js")
                .Include("~/Scripts/angularModules/ManageBO/Reviews/ReviewsCtrl.js")
                .Include("~/Scripts/angularModules/ManageBO/Soros/SorosCtrl.js")
                .Include("~/Scripts/angularModules/DayBooks/DayBooksListCtrl.js")
                .Include("~/Scripts/angularModules/DayBooks/DayBooksViewCtrl.js")
                .Include("~/Scripts/angularModules/DayBooks/DayBooksService.js")
                .Include("~/Scripts/angularModules/DayBooks/DayBooksCtrl.js")
                .Include("~/Scripts/angularModules/DayBooks/DayBookItemsService.js")
                .Include("~/Scripts/angularModules/DayBooks/DayBookItemsCtrl.js");

            css.Transforms.Clear();
            baseScripts.Transforms.Clear();

            bundles.Add(css);
            bundles.Add(baseScripts);
            bundles.Add(loginScripts);
            bundles.Add(applicationScripts);

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
