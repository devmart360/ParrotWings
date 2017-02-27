(function () {
    var app = angular.module('app');

    var controllerId = 'app.views.transaction.form';

    app.controller(controllerId, [
        '$scope', '$stateParams', 'abp.services.app.transaction', 'abp.services.app.user', '$location',
    function ($scope, $stateParams, transactionService, userService, $location) {
        var vm = this;
        vm.mode = $scope.mode;

        console.log('app.views.transaction.form start');

        vm.users = [];
        vm.trans = {};

        //vm.querySearch = querySearch;
        vm.selectedItemChange = selectedItemChange;
        vm.searchTextChange = searchTextChange;

        //function querySearch(query) {
        //    if (query) {
        //        return userService.find({
        //            searchTerm: angular.lowercase(query)
        //        })
        //        .success(function (data) {
        //            console.log(data);
        //            vm.users = data;
        //        });
        //        //return [{ "name": "test 1", "surname": "1", "userName": "test1", "fullName": "test 1 1", "emailAddress": "test1@pw.local", "isEmailConfirmed": true, "lastLoginTime": null, "isActive": true, "creationTime": "2017-02-23T20:20:50.58", "id": 3 },
        //        //    { "name": "test 2", "surname": "2", "userName": "test2", "fullName": "test 2 2", "emailAddress": "test2@pw.local", "isEmailConfirmed": true, "lastLoginTime": null, "isActive": true, "creationTime": "2017-02-23T20:21:11.453", "id": 4 }];

        //    }
        //    return [];
        //}

        function searchTextChange(text) {
            console.info('Text changed to ' + text);

            //userService.find({
            transactionService.find({
                searchTerm: angular.lowercase(text)
            })
            .success(function (data) {
                console.log(data);
                vm.users = data.items;
            });
        }

        function selectedItemChange(item) {
            console.info('Item changed to ' + JSON.stringify(item));
            vm.trans.recipientUserId = item.id;
        }

        // ---

        vm.send = function send() {
            abp.ui.setBusy(
               null,
               transactionService.create(
                   vm.trans
               ).success(function () {
                   console.log('Create transaction success');
                   abp.notify.success("Перевод выполнен");

                   $location.path('/transactions');
               })
            );
        };

        vm.cancel = function () {
            $location.path('/transactions');
        };
    }
    ]);

})();

