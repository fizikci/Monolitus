
var defaultListController = function ($scope, $routeParams, entityService) {

    if (!$scope.entityName) $scope.entityName = $routeParams.entityName;
    if (!$scope.orderBy) $scope.orderBy = 'InsertDate';
    if (!$scope.orderByAsc) $scope.orderByAsc = false;
    if (!$scope.pageSize) $scope.pageSize = 20;
	$scope.filter = {};
	if ($routeParams.where) {
        $scope.where = $routeParams.where;
		var parts = $routeParams.where.split(' AND ');
		for(var i=0; i<parts.length; i++){
			var keyVal = parts[i].split(' = ');
			$scope.filter[keyVal[0]] = keyVal[1];
		}
	}
    $scope.count = 20;

    if (!$scope.search)
        $scope.search = function () { $scope.getPage(0); };

    if (!$scope.tryFirstSearchCounter) $scope.tryFirstSearchCounter = 0;
    $scope.tryFirstSearch = function () {
        $scope.tryFirstSearchCounter--;
        if ($scope.tryFirstSearchCounter == 0)
            $scope.search();
    };

    $scope.setPages = function () {
        var pages = [];
        for (var i = Math.max(0, $scope.currPage - 4) ; i <= Math.min(Math.ceil($scope.count / $scope.pageSize) - 1, $scope.currPage + 4) ; i++)
            pages.push(i);
        $scope.pages = pages;
    };

    if (!$scope.getPage)
        $scope.getPage = function(pageNo) {
            if (pageNo!=0 && (pageNo < 0 || pageNo > Math.ceil($scope.count / $scope.pageSize) - 1))
                return;

            entityService.getList($scope.entityName, $scope.pageSize, pageNo, function(res) {
                $scope.$apply(function() {
                    $scope.list = res.list;
                    $scope.count = res.count;
                    $scope.currPage = pageNo;
                    $scope.setPages();
                });
            }, $scope.where, $scope.orderBy + ($scope.orderByAsc ? '' : ' desc'));
        };

    $scope.setOrderBy = function (f) {
        if ($scope.orderBy == f) {
            $scope.orderByAsc = !$scope.orderByAsc;
        } else {
            $scope.orderBy = f;
            $scope.orderByAsc = true;
        }
        $scope.getPage(0);
    };

    $scope.isOrderBy = function(f) {
        return $scope.orderByAsc && $scope.orderBy == f;
    };
    $scope.isOrderByDesc = function(f) {
        return !$scope.orderByAsc && $scope.orderBy == f;
    };

    $scope.delete = function (entity) {
        if (confirm('Kayıt silinecek!'))
            entityService.delete($routeParams.entityName, entity, function () {
                $scope.$apply(function () {
                    entity.IsDeleted = true;
                });
            });
    };
    $scope.undelete = function (entity) {
        if (confirm('Kayıt geri alınacak.'))
            entityService.undelete($routeParams.entityName, entity, function () {
                $scope.$apply(function () {
                    entity.IsDeleted = false;
                });
            });
    };

    if ($scope.tryFirstSearchCounter == 0)
        $scope.search();
};

var defaultEditController = function($scope, $routeParams, entityService) {

    entityService.get($routeParams.entityName, $routeParams.Id, function(entity) {
        $scope.$apply(function() {
            $scope.entity = entity;

            if ($scope.afterGet) $scope.afterGet();
        });
    });

    $scope.save = function() {
        entityService.save($routeParams.entityName, $scope.entity, function(e) {
            //location.href = '#/List/' + $routeParams.entityName;
            location.href = '#/View/' + $routeParams.entityName + '/' + e.Id;
        });
    };
    $scope.cancel = function() {
        location.href = '#/List/' + $routeParams.entityName;
    };
};

var defaultViewController = function ($scope, $routeParams, entityService) {
    if (!$scope.entityName)
        $scope.entityName = $routeParams.entityName;

    entityService.get($routeParams.entityName, $routeParams.Id, function (entity) {
        $scope.$apply(function () {
            $scope.entity = entity;

            for (var cs = $scope.$$childHead; cs; cs = cs.$$nextSibling) {
                if (cs.getPage) {
                    cs.where += entity.Id;
                    cs.getPage(0);
                }
            }

            if ($scope.afterGet) $scope.afterGet();
        });
    });

    $scope.delete = function () {
        if (confirm('Kayıt silinecek!'))
            entityService.delete($scope.entityName, $scope.entity, function () {
                $scope.$apply(function () {
                    $scope.entity.IsDeleted = true;
                });
            });
    };

    $scope.undelete = function () {
        if (confirm('Arşivden çıkarılacak!'))
            entityService.undelete($scope.entityName, $scope.entity, function () {
                $scope.$apply(function () {
                    $scope.entity.IsDeleted = false;
                });
            });
    };
    $scope.toggleDelete = function () {
        if ($scope.entity.IsDeleted)
            $scope.undelete();
        else
            $scope.delete();
    };
    //yayınlama işlemleri
    $scope.onayla = function () {
        if (confirm('Kayıt yayınlanacaktır!')) {
            $scope.entity.Onaylandi = true;
            entityService.save($scope.entityName, $scope.entity, function (e) {
                $scope.entity = e;
            });
        }
    };

    $scope.onayiKaldir = function () {
        if (confirm('Kayıt yayından kaldırılacaktır!')) {
            $scope.entity.Onaylandi = false;
            entityService.save($scope.entityName, $scope.entity, function (e) {
                $scope.entity = e;
            });
        }
    };
    $scope.toggleOnayla = function () {
        if ($scope.entity.Onaylandi)
            $scope.onayiKaldir();
        else
            $scope.onayla();
    };

    //Bülten alımı işlemleri   
    $scope.BultenAl = function () {
        if (confirm('Bülten Alınacaktır!')) {
            $scope.entity.BultenAlimi = true;
            entityService.save($scope.entityName, $scope.entity, function (e) {
                $scope.entity = e;
            });
        }
    };
    $scope.BultenAlma = function () {
        if (confirm('Bülten Alınmayacaktır!')) {
            $scope.entity.BultenAlimi = false;
            entityService.save($scope.entityName, $scope.entity, function (e) {
                $scope.entity = e;
            });
        }
    };

    $scope.toggleBultenAlimi = function () {
        if ($scope.entity.BultenAlimi)
            $scope.BultenAlma();
        else
            $scope.BultenAl();
    };
};

app.controller('APIDemoController', function ($scope, $routeParams, entityService) {});
app.controller('DashboardController', function ($scope, $routeParams, entityService) {

    $scope.toUnixTimestamp = function (date) {
        return Math.round(date.getTime() / 1000);
    };

    $scope.showReport = function (reportName, label) {
        doAjaxCall("/Staff/Handlers/Report.ashx?report="+reportName, function (res) {
            $scope.$apply(function () {
                $scope.interval = 'day';
                $scope.data = res;
                $scope.ehbmSel = label;
            });
        });
    };

    $scope.showReport('UsersForLastWeek', 'This Week');
});

var viewDetailListController = function ($scope, $routeParams, entityService) {
    //if (($scope.orderBy)!== null) $scope.orderBy = 'OrderNo';
    if (!$scope.orderBy) $scope.orderBy = 'Id';
    $scope.orderByAsc = true;
    if (!$scope.pageSize) $scope.pageSize = 20;
    $scope.count = 20;

    $scope.setPages = function () {
        var pages = [];
        for (var i = Math.max(0, $scope.currPage - 4) ; i <= Math.min(Math.ceil($scope.count / $scope.pageSize) - 1, $scope.currPage + 4) ; i++)
            pages.push(i);
        $scope.pages = pages;
    };

    $scope.getPage = function (pageNo) {
        if (!$scope.entityName)
            return;
        if (pageNo < 0 || pageNo > Math.ceil($scope.count / $scope.pageSize) - 1)
            return;

        entityService.getList($scope.entityName, $scope.pageSize, pageNo, function (res) {
            $scope.$apply(function () {
                $scope.list = res.list;
                $scope.count = res.count;
                $scope.currPage = pageNo;
                $scope.setPages();
            });
        }, $scope.where, $scope.orderBy + ($scope.orderByAsc ? '' : ' desc'));
    };

    $scope.setOrderBy = function (f) {
        if ($scope.orderBy == f) {
            $scope.orderByAsc = !$scope.orderByAsc;
        } else {
            $scope.orderBy = f;
            $scope.orderByAsc = true;
        }
        $scope.getPage(0);
    };

    $scope.isOrderBy = function (f) {
        return $scope.orderByAsc && $scope.orderBy == f;
    };
    $scope.isOrderByDesc = function (f) {
        return !$scope.orderByAsc && $scope.orderBy == f;
    };

    $scope.delete = function (entity) {
        if (confirm('Record is to be deleted!'))
            entityService.delete($scope.entityName, entity, function () {
                $scope.$apply(function () {
                    $scope.getPage($scope.currPage);
                });
            });
    };

    //$scope.$watch('pageSize', function (v) { $scope.getPage(0); });
};

app.controller('ViewDetailListController', viewDetailListController);


app.controller('ListUserController', function ($scope, $routeParams, entityService) {
    $scope.search = function () {
		$scope.where = setCriteriaValue($scope.where, 'UserType', $scope.filter.UserType);
		$scope.where = setCriteriaValue($scope.where, 'IsDeleted', $scope.filter.IsDeleted);
        $scope.getPage(0);
    };

    entityService.getEnumList('UserTypes', function (res) {
        $scope.$apply(function () {
            $scope.UserTypeList = res;
        });
    });
    var secenek = [{ "1": "Active " }, { "0": "Passive " }];
    $scope.Durum = secenek.reduce(function (id, val) {
        return angular.extend(id, val);
    }, {});
    $scope.orderBy = 'InsertDate';
    defaultListController($scope, $routeParams, entityService);
});
app.controller('EditUserController', function ($scope, $routeParams, entityService) {

    $scope.afterGet = function () {
        entityService.getEnumList('UserTypes', function (res) {
            $scope.$apply(function () {
                $scope.EnumUserTypes = res;
            });
        });
        entityService.getEnumList('Cinsiyet', function (res) {
            $scope.$apply(function () {
                $scope.EnumCinsiyet = res;
            });
        });
    };

    defaultEditController($scope, $routeParams, entityService);
});
app.controller('ViewUserController', function ($scope, $routeParams, entityService) {
    $scope.afterGet = function () {
        if (!$scope.entity.Avatar.startsWith('http'))
            $scope.entity.Avatar = 'http://api.monolit.us/Medya/Avatars/' + $scope.entity.Avatar;
    };
    defaultViewController($scope, $routeParams, entityService);
});

function setCriteriaValue(where, field, value) {
	if(!where) where = '';
	if(value!=null){
		if(where.indexOf(field)==-1){
			return where + ((where ? ' AND ' : '') + field + ' = ' + value);
		} else {
			var parts = where.split(' AND ');
			for(var i=0; i<parts.length; i++){
				var keyVal = parts[i].split(' = ');
				if(keyVal[0]==field)
					parts[i] = keyVal[0] + ' = ' + value;
			}
			return parts.join(' AND ');
		}
	} else {
		if(where.indexOf(field)==-1){
			return where;
		} else {
			var parts = where.split(' AND ');
			for(var i=0; i<parts.length; i++){
				var keyVal = parts[i].split(' = ');
				if(keyVal[0]==field){
					parts.splice(i);
				}
			}
			return parts.join(' AND ');
		}
	}
}
