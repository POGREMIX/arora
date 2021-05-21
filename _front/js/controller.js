'use strict';

/* Controllers */

var guestbookCtrl = angular.module('guestbook.Controllers', []);

guestbookCtrl.controller('MessageListController', ['$scope', 'rest', '$routeParams', '$location',
	function ($scope, rest, $routeParams, $location) {
	    $scope.title = "Message list";
		
		// это пример использования сервера rest для выполнения запроса к API;
		// параметром служит callback-функция, которая будет вызвана после получения ответа от сервера
	    rest.loadMessageList(function(data) { $scope.list = data.answer; });
	}
]);

guestbookCtrl.controller('MessageViewController', ['$scope', 'rest', '$routeParams', '$location', '$window',
	function ($scope, rest, $routeParams, $location, $window) {
	    $scope.title = "View message";
		var messageID = $routeParams.messageID;
	    rest.loadMessage(messageID, function(data) { $scope.message = data.answer; });
		
		$scope.newComment = { MessageID : messageID, };
		
		$scope.addComment = function() {
			rest.saveComment($scope.newComment, function(data) {
				rest.loadMessage(messageID, function(data) { $scope.message = data.answer; });
				$scope.newComment = { MessageID : messageID, };
				$window.scrollTo(0, 0);
			});
		};
	}
]);

guestbookCtrl.controller('MessageEditController', ['$scope', 'rest', '$routeParams', '$location',
	function ($scope, rest, $routeParams, $location) {
		var messageID = $routeParams.messageID;
		if (messageID) {
			$scope.title = "Edit message";
			rest.loadMessage(messageID, function(data) { $scope.message = data.answer; });
		} else {
			$scope.title = "Add message";
			$scope.message = {};
		}
		
		$scope.saveMessage = function() {
			rest.saveMessage($scope.message, function(data) {
				$location.path('/message/view/'+data.answer.ID);
			});
		}
	}
]);

guestbookCtrl.controller('MessageDeleteController', ['$scope', 'rest', '$routeParams', '$location',
	function ($scope, rest, $routeParams, $location) {
	    $scope.title = "Delete message";
		var messageID = $routeParams.messageID;
		if (messageID) {
			rest.loadMessage(messageID, function(data) { $scope.message = data.answer; });
		} else {
			$location.path('/home');
		}
		
		$scope.deleteMessage = function() {
			rest.deleteMessage($scope.message.ID, function(data) {
				$location.path('/home');
			});
		}
	}
]);

guestbookCtrl.controller('MessageVoteController', ['$scope', 'rest', '$routeParams', '$location', '$window',
	function ($scope, rest, $routeParams, $location, $window) {
	    $scope.title = "Vote for message";
		var messageID = $routeParams.messageID;
	    rest.loadMessage(messageID, function(data) { $scope.message = data.answer; });
		
		$scope.rating = 0;
		
		$scope.addRating = function() {
			if ($scope.rating < 1) return;
			rest.voteMessage($scope.message.ID, $scope.rating, function(data) {
				$location.path('/message/view/'+$scope.message.ID);
			});
		};
	}
]);

guestbookCtrl.controller('CommentViewController', ['$scope', 'rest', '$routeParams', '$location',
	function ($scope, rest, $routeParams, $location) {
	    $scope.title = "View comment";
		var commentID = $routeParams.commentID;
	    rest.loadComment(commentID, function(data) { $scope.comment = data.answer; });
	}
]);

guestbookCtrl.controller('CommentEditController', ['$scope', 'rest', '$routeParams', '$location',
	function ($scope, rest, $routeParams, $location) {
	    $scope.title = "Edit comment";
		var commentID = $routeParams.commentID;
		if (commentID) {
			rest.loadComment(commentID, function(data) { $scope.comment = data.answer; });
		} else {
			$scope.comment = {};
		}
		
		$scope.saveComment = function() {
			rest.saveComment($scope.comment, function(data) {
				$location.path('/comment/view/'+commentID);
			});
		}
	}
]);
