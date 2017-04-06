
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

    $scope.showReport('EtkinlikHistoryForLastWeek', 'Bu Hafta');

    doAjaxCall("/Staff/Handlers/Report.ashx?report=UserDistribution", function (res) {
        jQuery('#vmapUserCount').vectorMap({
            map: 'world_en',
            backgroundColor: 'transparent',
            color: '#ffffff',
            hoverOpacity: 0.7,
            selectedColor: '#666666',
            enableZoom: true,
            showTooltip: true,
            values: res,
            scaleColors: ['#C8EEFF', '#006491'],
            normalizeFunction: 'polynomial',
            onLabelShow: function (element, label, code) {
                if (res[code])
                    label.text(label.text() + ' (' + res[code] + ")");
            }
        });
    });

    doAjaxCall("/Staff/Handlers/Report.ashx?report=EtkinlikHistoryMapForLastWeek", function (res) {
        jQuery('#vmapEtkCountWeek').vectorMap({
            map: 'world_en',
            backgroundColor: 'transparent',
            color: '#ffffff',
            hoverOpacity: 0.7,
            selectedColor: '#666666',
            enableZoom: true,
            showTooltip: true,
            values: res,
            scaleColors: ['#C8EEFF', '#006491'],
            normalizeFunction: 'polynomial',
            onLabelShow: function (element, label, code) {
                if (res[code])
                    label.text(label.text() + ' (' + res[code] + ")");
            }
        });
    });
});

app.controller('ListViewEntityLocaleAllController', function ($scope, $routeParams, entityService) {

    $scope.orderBy = 'EntityName';

    $scope.search = function () {
        $scope.where = setCriteriaValue($scope.where, 'EntityName', $scope.filter.EntityName);
        $scope.where = setCriteriaValue($scope.where, 'FieldName', $scope.filter.FieldName);
        $scope.getPage(0);
    };

    $scope.EntityNameList = [{ Id: 'DersKitabi', Name: 'Ders Kitabı' }, { Id: 'Unite', Name: 'Ünite' }, { Id: 'Etkinlik', Name: 'Etkinlik' }, { Id: 'EtkinlikTuru', Name: 'Etkinlik Türü' }];
    $scope.FieldNameList = [{ Id: 'Name', Name: 'Adı' }, { Id: 'Description', Name: 'Açıklaması' }];

    defaultListController($scope, $routeParams, entityService);

    $scope.edit = function (e, langName, langId) {
        var html = '<textarea id="loc" style="width:500px;height:200px">' + (e[langName] || '') + '</textarea><br/>'
        alertNice(html, '', '<button id="btnKydt" type="button" class="btn btn-success" data-dismiss="modal">Kaydet</button>');

        e.langName = langName;
        e.langId = langId;

        $('#btnKydt').click(function () {
            e[langName] = $('#loc').val();
            doAjaxCall('/Staff/Handlers/DoCommand.ashx?method=saveLocale', function (entity) {}, e);
        });
    }
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
        if (confirm('Kayıt silinecek!'))
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
    var secenek = [{ "1": "Aktif Kayıtlar " }, { "0": "Pasif Kayıtlar" }];
    $scope.Durum = secenek.reduce(function (id, val) {
        return angular.extend(id, val);
    }, {});
    $scope.orderBy = 'Name';
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
    defaultViewController($scope, $routeParams, entityService);
});

app.controller('ListCompanyController', function ($scope, $routeParams, entityService) {
    $scope.search = function () {
        $scope.where = setCriteriaValue($scope.where, 'SectorId', $scope.filter.SectorId);
        $scope.where = setCriteriaValue($scope.where, 'IsDeleted', $scope.filter.IsDeleted);
        $scope.getPage(0);
    };

    entityService.getList('Sector', 1000, 0, function (res) {
        $scope.$apply(function () {
            $scope.SectorList = res.list;
        });
    }, '', 'Name');

    var secenek = [{ "1": "Aktif Kayıtlar " }, { "0": "Pasif Kayıtlar" }];
    $scope.Durum = secenek.reduce(function (id, val) {
        return angular.extend(id, val);
    }, {});
    $scope.orderBy = 'Name';
    $scope.orderByAsc = true;
    defaultListController($scope, $routeParams, entityService);
});
app.controller('EditCompanyController', function ($scope, $routeParams, entityService) {

    $scope.afterGet = function () {
        entityService.getList('Sector', 1000, 0, function (res) {
            $scope.$apply(function () {
                $scope.Sectors = res.list;
            });
        }, null, 'Name');
    };

    defaultEditController($scope, $routeParams, entityService);
});

app.controller('ListCompanyApplicationController', function ($scope, $routeParams, entityService) {
    $scope.search = function () {
        $scope.where = setCriteriaValue($scope.where, 'Applied', $scope.filter.Applied);
        $scope.getPage(0);
    };

    var secenek = [{ "1": "Kabul edilen başvurular" }, { "0": "Bekleyen başvurular" }];
    $scope.Durum = secenek.reduce(function (id, val) {
        return angular.extend(id, val);
    }, {});
    $scope.orderBy = 'InsertDate';
    $scope.orderByAsc = false;


    defaultListController($scope, $routeParams, entityService);
});


app.controller('ViewDetailListUserCompanyController', function ($scope, $routeParams, entityService) {

    $scope.toggle = function (entity, type) {
        entity[type] = !entity[type];
        entityService.save("UserCompany", entity, function (e) {
        });
    };

    viewDetailListController($scope, $routeParams, entityService);
});
app.controller('ViewDetailListCompanyKampanyaController', function ($scope, $routeParams, entityService) {

    $scope.yeni = {};

    $scope.save = function () {
        $scope.yeni.CompanyId = $routeParams.Id;
        entityService.save("CompanyKampanya", $scope.yeni, function (e) {
            $scope.$apply(function () {
                $scope.getPage($scope.currPage);
                $scope.yeni = {};
            });
        });
    };

    $scope.show = function (e) { $scope.yeni = e;}

    viewDetailListController($scope, $routeParams, entityService);
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
