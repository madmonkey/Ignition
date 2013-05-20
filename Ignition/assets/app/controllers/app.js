
//This configures the routes and associates each route with a view and a controller
//app.config(function ($routeProvider) {
//    $routeProvider
//        .when('/companies',
//            {
//                controller: 'CompaniesController',
//                templateUrl: '/app/partials/companies.html'
//            })
//        //Define a route that has a route parameter in it (:company)
//        .when('/companies/:company',
//            {
//                controller: 'CompaniesController',
//                templateUrl: '/app/partials/companies.html'
//            })
//        //Define a route that has a route parameter in it (:customerID)
//        .when('/audit',
//            {
//                controller: 'AuditController',
//                templateUrl: '/app/partials/audit.html'
//            })
//        .when('/audit/:pg, :limit',
//            {
//                controller: 'AuditController',
//                templateUrl: '/app/partials/audit.html'
//            })
//        .otherwise({ redirectTo: '/customers' });
//});