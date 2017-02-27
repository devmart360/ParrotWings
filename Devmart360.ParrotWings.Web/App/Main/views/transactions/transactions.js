(function () {
    var app = angular.module('app');

    var controllerId = 'app.views.transactions';

    app.controller(controllerId, [
        '$scope', '$stateParams', 'abp.services.app.transaction',
        function ($scope, $stateParams, transactionService) {
            var vm = this;

            vm.transactions = [];
            vm.balance = 0;

            function getAll() {
                abp.ui.setBusy(
                    null,
                    transactionService.getAll()
                        .success(function (result) {
                            vm.transactions = result.transactions;
                            console.log('Get all transactions success');
                        })
                );
            };
            getAll();

            // баланс по кнопке
            vm.getBalance = function () {
                transactionService.getBalance().success(function (data) {
                    abp.notify.success("Баланс: " + data);
                });
            }

            // баланс при старте
            transactionService.getBalance().success(function (data) {
                vm.balance = data;
            });

            // баланс актуальный
            abp.event.on('abp.notifications.received', function (userNotification) {
                if (userNotification.notification.notificationName === 'BalanceChanged') {
                    vm.balance = userNotification.notification.data.balance;
                    $scope.$apply();
                }
            });
        }
    ]);

})();