function TodoCtrl($scope, $http) {
    //$scope.todos = [
    //  { text: 'learn angular', done: true },
    //  { text: 'build an angular app', done: false }];
    
    $http.get('api/todos').success(function (data) {
        $scope.todos = data;
    });

    $scope.addTodo = function () {
        $scope.todos.push({ Description: $scope.todoText, Done: false });
        $scope.todoText = '';
    };

    $scope.remaining = function () {
        var count = 0;
        angular.forEach($scope.todos, function (todo) {
            count += todo.Done ? 0 : 1;
        });
        return count;
    };

    $scope.archive = function () {
        var oldTodos = $scope.todos;
        $scope.todos = [];
        angular.forEach(oldTodos, function (todo) {
            if (!todo.Done) $scope.todos.push(todo);
        });
    };
}