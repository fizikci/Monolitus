var app = angular.module('dealerSafeApp', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/APIDemo',
            {
                controller: function (params) { return 'APIDemoController'; },
                templateUrl: function (params) { return '/Staff/Partials/APIDemo.aspx'; }
            })
        .when('/List/:entityName/:where?',
            {
                controller: function (params) {
                    var ctrlName = 'List' + params.entityName + 'Controller';
                    var ctrlList = app._invokeQueue.map(function (v, k) { return (typeof v[2][0] == 'string') ? v[2][0] : ''; });
                    if (ctrlList.indexOf(ctrlName) >= 0)
                        return ctrlName;
                    else
                        return 'defaultListController';
                },
                templateUrl: function (params) { return '/Staff/Partials/List' + params.entityName + '.aspx'; }
            })
        .when('/Edit/:entityName/:Id?',
            {
                controller: function (params) {
                    var ctrlName = 'Edit' + params.entityName + 'Controller';
                    var ctrlList = app._invokeQueue.map(function (v, k) { return (typeof v[2][0] == 'string') ? v[2][0] : ''; });
                    if (ctrlList.indexOf(ctrlName) >= 0)
                        return ctrlName;
                    else
                        return 'defaultEditController';
                },
                templateUrl: function (params) { return '/Staff/Partials/Edit' + params.entityName + '.aspx?Id=' + params.Id; }
            })
        .when('/View/:entityName/:Id',
            {
                controller: function (params) {
                    var ctrlName = 'View' + params.entityName + 'Controller';
                    var ctrlList = app._invokeQueue.map(function (v, k) { return (typeof v[2][0] == 'string') ? v[2][0] : ''; });
                    if (ctrlList.indexOf(ctrlName) >= 0)
                        return ctrlName;
                    else
                        return 'defaultViewController';
                },
                templateUrl: function (params) { return '/Staff/Partials/View' + params.entityName + '.aspx?Id=' + params.Id; }
            })
        .otherwise({
            controller: function (params) { return 'DashboardController'; },
            templateUrl: function (params) { return '/Staff/Partials/Dashboard.aspx'; }
        });
}).config(function($sceProvider) {
  $sceProvider.enabled(false);
});

app.factory('audio',function ($document) {
    var audioElement = $document[0].createElement('audio'); // <-- Magic trick here
      
    var res = {
        audioElement: audioElement,
        end: function(){
            // to be set by user
        },
        play: function(filename) {
            audioElement.src = filename;
            audioElement.play();     //  <-- Thats all you need
        },
        pause: function() {
            audioElement.pause();     //  <-- Thats all you need
        },
        isPlaying: function(){
            return !audioElement.paused;
        }

    };
      
    audioElement.addEventListener("ended", function() { 
        res.end(); 
    }, true);
      
    return res;
});