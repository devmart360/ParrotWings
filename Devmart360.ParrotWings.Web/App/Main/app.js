(function () {
    'use strict';
    
    var app = angular.module('app', [
        'ngRoute',
        'ngAnimate',
        'ngSanitize',

        'ngMaterial',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/');

            if (abp.auth.hasPermission('Pages.Users')) {
                $stateProvider
                    .state('users', {
                        url: '/users',
                        templateUrl: '/App/Main/views/users/index.cshtml',
                        menu: 'Users' //Matches to name of 'Users' menu in ParrotWingsNavigationProvider
                    });
                $urlRouterProvider.otherwise('/users');
            }

            if (abp.auth.hasPermission('Pages.Tenants')) {
                $stateProvider
                    .state('tenants', {
                        url: '/tenants',
                        templateUrl: '/App/Main/views/tenants/index.cshtml',
                        menu: 'Tenants' //Matches to name of 'Tenants' menu in ParrotWingsNavigationProvider
                    });
                $urlRouterProvider.otherwise('/tenants');
            }

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in ParrotWingsNavigationProvider
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in ParrotWingsNavigationProvider
                })
                .state('transactions', {
                    url: '/transactions',
                    templateUrl: '/App/Main/views/transactions/transactions.cshtml',
                    menu: 'Transactions' //Matches to name of 'Transactions' menu in ParrotWingsNavigationProvider
                })
                .state('newTrans', {
                     url: '/transactions/new', 
                     templateUrl: '/App/Main/views/transactions/transaction.form.cshtml',
                     menu: 'Transactions'
                 })
            ;
        }
    ]);


    app.controller('newTransactionController', [
       '$scope', '$state', '$stateParams', 'abp.services.app.transaction',
       function ($scope, $state, $stateParams, transactionService) {
           console.log('newTransactionController start');
       }]);

})();