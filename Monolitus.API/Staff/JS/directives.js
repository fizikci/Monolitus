app.directive('listHeader', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            return  '<div class="page-header">' +
                    '  	<h1>'+attr.title+' <small> <i class="icon-double-angle-right"></i> {{count | number}} kayıt </small></h1>' +
						elm.html() +
                    '</div>';
        }
    };
});

app.directive('pagination', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            return  '<ul class="pagination pull-right">' +
                    '    <li ng-class="{prev:true, disabled:currPage==0}"><a ng-click="getPage(currPage-1)"><i class="icon-double-angle-left"></i></a></li>' +
                    '    <li ng-repeat="i in pages" ng-class="{active:i==currPage}"><a ng-click="getPage(i)">{{i+1}}</a></li>' +
                    '    <li class="{next:true, disabled:currPage==count / pageSize}"><a ng-click="getPage(currPage+1)"><i class="icon-double-angle-right"></i></a></li>' +
                    '</ul>';
        }
    };
});

app.directive('pageSize', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            return '<select ng-model="pageSize" ng-options="i for i in [10,20,50]" class="pull-right" ng-change="search()" style="margin: 20px 0 20px 20px;height: 31px;background: #FAFAFA;"></select>';
        }
    };
});

app.directive('columnHeader', function () {
    return {
        restrict: 'A',
        replace: true,
        template: function (elm, attr) {
            return '<th ng-click="setOrderBy(\'' + attr.field + '\')">' + attr.columnHeader + ' <i ng-if="isOrderBy(\'' + attr.field + '\')" class="icon-caret-up"></i><i ng-if="isOrderByDesc(\'' + attr.field + '\')" class="icon-caret-down"></i></th>';
        }
    };
});

app.directive('indexer', function () {
    return {
        restrict: 'A',
        replace: true,
        template: function (elm, attr) {
            return '<td>{{$index + currPage * pageSize + 1}}</td>';
        }
    };
});

app.directive('linkToView', function () {
    return {
        restrict: 'A',
        replace: true,
        template: function (elm, attr) {
            return '<td><a href="#/View/{{entityName}}/{{entity.Id}}">' + elm.html() + '</a></td>';
        }
    };
});

app.directive('linkToEdit', function () {
    return {
        restrict: 'A',
        replace: true,
        template: function (elm, attr) {
            return '<td><a href="#/Edit/{{entityName}}/{{entity.Id}}">' + elm.html() + '</a></td>';
        }
    };
});

app.directive('linkToChildren', function () {
    return {
        restrict: 'A',
        replace: true,
        template: function (elm, attr) {
            return '<td><a href="#/List/' + attr.linkToChildren + '/{{entityName}}Id = {{entity.Id}}">' + elm.html() + '</a></td>';
        }
    };
});

app.directive('linkToParent', function () {
    return {
        restrict: 'A',
        replace: true,
        template: function (elm, attr) {
            var ref = attr.refFieldName;
            if (!ref) ref = attr.linkToParent + "Id";
            return '<td><a href="#/View/' + attr.linkToParent + '/{{entity.'+ref+'}}">' + elm.html() + '</a></td>';
        }
    };
});

app.directive('operations', function () {
    return {
        restrict: 'A',
        replace: true,
        template: function (elm, attr) {
            return '<td>' +
                    (attr.edit != 'off' ? '    <a class="dtBtn orange" ng-show="!entity.IsDeleted" href="#/Edit/{{entityName}}/{{entity.Id}}"><i class="icon-pencil bigger-130" title="Edit"></i></a>' : '') +
                    (attr.delete != 'off' ? '    <a class="dtBtn red" ng-show="!entity.IsDeleted" ng-click="delete(entity)"><i class="icon-trash bigger-130" ></i></a> <a class="dtBtn green" ng-show="entity.IsDeleted" ng-click="undelete(entity)"><i class="icon-undo bigger-130" ></i></a>' : '') +
                        elm.html() +
                    '</td>';
        }
    };
});

app.directive('infoRow', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            return '<div class="profile-info-row">' +
                    '    <div class="profile-info-name">' + attr.label + '</div>' +
                    '    <div class="profile-info-value">' +
                    elm.html() +
                    '    </div>' +
                    '</div>';
        }
    };
});

app.directive('listFooter', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            return '<div class="clearfix form-actions text-right">' +
                    (attr.noAddNew ? '' : '    <a class="btn btn-xs btn-primary" href="#/Edit/{{entityName}}"><i class="icon-plus bigger-110"></i> Add New</a> &nbsp;') +
                    (attr.noExport ? '' : '    <a class="btn btn-xs btn-info" type="button" href="#/Excel/{{entityName}}"><i class="icon-external-link bigger-110"></i> Export to Excel</a>') +
                    elm.html() +
                    '</div>';
        }
    };
});

/*
 * Input directives
*/
app.directive('inputText', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            if (!attr.placeholder) attr.placeholder = '';
            return '<div>' +
                    '   <div class="space-4"></div>' +
                    '   <div class="form-group">' +
                    '       <label for="' + attr.name + '" class="col-sm-3 control-label no-padding-right"> ' + attr.label + ' </label>' +
                    '       <div class="col-sm-9">' +
                    '           <input type="text" ng-model="' + attr.model + '"'+(attr.name ? ' name="' + attr.name + '" id="' + attr.name + '"' : '')+' placeholder="' + attr.placeholder + '"'+(attr.disabled?' disabled="'+attr.disabled+'"':'')+' class="col-sm-10 col-xs-12" />' +
                        elm.html() +
                    '       </div>' +
                    '   </div>' +
                    '</div>';
        }
    };
});
app.directive('inputNumber', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            return '<div>' +
                    '   <div class="space-4"></div>' +
                    '   <div class="form-group">' +
                    '       <label for="' + attr.name + '" class="col-sm-3 control-label no-padding-right"> ' + attr.label + ' </label>' +
                    '       <div class="col-sm-9">' +
                    '           <input type="number" ng-model="' + attr.model + '"' + (attr.name ? ' name="' + attr.name + '" id="' + attr.name + '"' : '') + (attr.disabled ? ' disabled="' + attr.disabled + '"' : '') + ' class="input-mini bkspinner" />' +
                        elm.html() +
                    '       </div>' +
                    '   </div>' +
                    '</div>';
        }
    };
});
app.directive('inputTextarea', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            return '<div>' +
                    '   <div class="space-4"></div>' +
                    '   <div class="form-group">' +
                    '       <label for="' + attr.name + '" class="col-sm-3 control-label no-padding-right"> ' + attr.label + ' </label>' +
                    '       <div class="col-sm-9">' +
                    '           <textarea ng-model="' + attr.model + '"' + (attr.name ? ' name="' + attr.name + '" id="' + attr.name + '"' : '') + (attr.disabled ? ' disabled="' + attr.disabled + '"' : '') + ' class="col-sm-10 col-xs-12" rows="3"></textarea>' +
                        elm.html() +
                    '       </div>' +
                    '   </div>' +
                    '</div>';
        }
    };
});
app.directive('inputFile', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            if (!attr.placeholder) attr.placeholder = '';
            return '<div>' +
                    '   <div class="space-4"></div>' +
                    '   <div class="form-group">' +
                    '       <label for="' + attr.name + '" class="col-sm-3 control-label no-padding-right"> ' + attr.label + ' </label>' +
                    '       <div class="col-sm-8 input-group">' +
                    '           <input class="col-sm-12 col-xs-12" type="text" ng-model="' + attr.model + '"' + (attr.name ? ' name="' + attr.name + '" id="' + attr.name + '"' : '') + (attr.disabled ? ' disabled="' + attr.disabled + '"' : '') + ' placeholder="' + attr.placeholder + '" />' +
                    '           <span class="input-group-addon" onclick="openFileManager($(\'#' + attr.name + '\').val(), function (path) {$(\'#' + attr.name + '\').val(path); $(\'#' + attr.name + '\').trigger(\'input\');});"><i class="icon-folder-open"></i></span>' +
                    '           <span class="input-group-addon" onclick="$.colorbox({href:$(\'#' + attr.name + '\').val()});"><i class="icon-play"></i></span>' +
                        elm.html() +
                    '       </div>' +
                    '   </div>' +
                    '</div>';
        }
    };
});

app.directive('inputSelect', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            var horizontal = attr.horizontal;
            var res =
                    (!horizontal ? '<div>' : '') +
                    (!horizontal ? '   <div class="space-4"></div>' : '') +
                    '   <div class="form-group">' +
                    '       <label for="' + attr.name + '" class="' + (horizontal ? '' : 'col-sm-3') + ' control-label no-padding-right"> ' + attr.label + ' </label>' +
                '       <div' + (horizontal ? '' : ' class="col-sm-9"') + '>' +
                '           <select' + (attr.name ? ' id="' + attr.name + '"' : '') + ' ng-model="' + attr.model + '" ng-options="' + attr.options + '"' + (attr.change ? ' ng-change="' + attr.change + '"' : '') + (horizontal ? '' : ' class="col-sm-10 col-xs-12"') +(attr.disabled?' disabled="'+attr.disabled+'"':'') + '>' +
                    (attr.noEmptyOption ? '' : '               <option value="" class=""></option>') +
                    '           </select>' +
                '           <input type="text" style="display:none" ng-model="' + attr.model + '"' + (attr.name ? ' name="' + attr.name + '"' : '') + ' />' +
                        elm.html() +
                    '       </div>' +
                    '   </div>' +
                    (!horizontal ? '</div>' : '');
            return res;
        }
    };
});
app.directive('inputDate', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            if (!attr.placeholder) attr.placeholder = '';
            return '<div>' +
                    '   <div class="space-4"></div>' +
                    '   <div class="form-group">' +
                    '       <label for="' + attr.name + '" class="col-sm-3 control-label no-padding-right"> ' + attr.label + ' </label>' +
                    '       <div class="col-sm-8 input-group">' +
                    '           <input type="text" class="date-picker col-sm-12 col-xs-12" ng-model="' + attr.model + '"' + (attr.name ? ' name="' + attr.name + '" id="' + attr.name + '"' : '') + (attr.disabled ? ' disabled="' + attr.disabled + '"' : '') + ' data-date-format="yyyy-mm-dd" />' +
                    '           <span class="input-group-addon"><i class="icon-calendar"></i></span>' +
                        elm.html() +
                    '       </div>' +
                    '   </div>' +
                    '</div>';
        },
        link: function (scope, element, attrs, controllers) {
            setTimeout(function () {
                var v = element.find(".date-picker").val();
                if (v.indexOf('T') > -1)
                    element.find(".date-picker").val(v.split('T')[0]);
            }, 200);
            element.find(".date-picker").datepicker();
        }
    };
});
app.directive('inputCheck', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            if (!attr.placeholder) attr.placeholder = '';
            return '<div>' +
                    '   <div class="space-4"></div>' +
                    '   <div class="form-group">' +
                    '       <label for="' + attr.name + '" class="col-sm-3 control-label no-padding-right"> ' + attr.label + ' </label>' +
                    '       <div class="col-sm-8">' +
                    '           <input type="checkbox" ng-model="' + attr.model + '"' + (attr.name ? ' name="' + attr.name + '" id="' + attr.name + '"' : '') + (attr.disabled ? ' disabled="' + attr.disabled + '"' : '') + ' class="ace ace-switch ace-switch-5" />' +
                    '           <span class="lbl"></span>' +
                        elm.html() +
                    '       </div>' +
                    '   </div>' +
                    '</div>';
        }
    };
});

app.directive('inputLang', function () {
    return {
        restrict: 'E',
        replace: true,
        template: function (elm, attr) {
            return '<span ng-click="localize(\''+attr.fieldName+'\')" ng-show="localization.'+attr.fieldName+'.length" class="label label-success arrowed" style="cursor:pointer">'+
					'	{{localization.'+attr.fieldName+'.length}} çeviri'+
					'</span>';
        }
    };
});


app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});

app.directive('timeChart', function () {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs) {
            var chart = null;
            var options = {
                xaxis: { mode: 'time' },
                grid: {backgroundColor: attrs.backgroundColor},
                series: {lines:{lineWidth:1}}
            };
            if (attrs.interval) options.xaxis.minTickSize = [1, attrs.interval];

            // If the data changes somehow, update it in the chart
            scope.$watch('data', function (v) {
                if (!v) return;
                options.xaxis.minTickSize = [1, scope.interval];
                $.plot(elem, [v], options);
                elem.show();
            });
        }
    };
});

//This directive adds custom animations to views as they enter or leave a screen
//Note that AngularJS 1.1.4 now has an ng-animate directive but this one can be used when you 
//want complete control or when you can't use that version of AngularJS yet
app.directive('animatedView', ['$route', '$anchorScroll', '$compile', '$controller', function ($route, $anchorScroll, $compile, $controller) {
    return {
        restrict: 'ECA',
        terminal: true,
        link: function (scope, element, attr) {
            var lastScope,
                onloadExp = attr.onload || '',
                defaults = { duration: 500, viewEnterAnimation: 'slideLeft', viewExitAnimation: 'fadeOut', slideAmount: 50, disabled: true },
                locals,
                template,
                options = scope.$eval(attr.animations);

            angular.extend(defaults, options);

            scope.$on('$routeChangeSuccess', update);
            update();


            function destroyLastScope() {
                if (lastScope) {
                    lastScope.$destroy();
                    lastScope = null;
                }
            }

            function clearContent() {
                element.html('');
                destroyLastScope();
            }

            function update() {
                updateNavAndBreadCrumb();

                locals = $route.current && $route.current.locals;
                template = locals && locals.$template;

                if (template) {
                    if (!defaults.disabled) {
                        if (element.children().length > 0) { //Have content in view
                            animate(defaults.viewExitAnimation);
                        }
                        else { //No content in view so treat it as an enter animation
                            animateEnterView(defaults.viewEnterAnimation);
                        }
                    }
                    else {
                        bindElement();
                    }

                } else {
                    clearContent();
                }
            }

            function animateEnterView(animation) {
                $(element).css('display', 'block');
                bindElement();
                animate(animation);
            }

            function animate(animationType) {
                switch (animationType) {
                    case 'fadeOut':
                        $(element.children()).animate({
                            //opacity: 0.0, 
                        }, defaults.duration, function () {
                            animateEnterView('slideLeft');
                        });
                        break;
                    case 'slideLeft':
                        $(element.children()).animate({
                            left: '-=' + defaults.slideAmount,
                            opacity: 1.0
                        }, defaults.duration);
                        break;
                    case 'slideRight':
                        $(element.children()).animate({
                            left: '+=' + defaults.slideAmount,
                            opacity: 1.0
                        }, defaults.duration);
                        break;
                }
            }

            function bindElement() {
                element.html(template);
                destroyLastScope();

                var link = $compile(element.contents()),
                    current = $route.current,
                    controller;

                lastScope = current.scope = scope.$new();
                if (current.controller) {
                    locals.$scope = lastScope;
                    var controllerName = typeof(current.controller) == 'function' ? current.controller($route.current.params) : current.controller;
                    controller = $controller(controllerName, locals);
                    element.children().data('$ngControllerController', controller);
                }

                link(lastScope);
                lastScope.$emit('$viewContentLoaded');
                lastScope.$eval(onloadExp);

                // $anchorScroll might listen on event...
                $anchorScroll();
            }

            function updateNavAndBreadCrumb() {
                $(".nav li").removeClass('active');
                //$(".nav li").removeClass('open');
                //$(".nav ul.submenu").css('display','none');

                $("a[href='" + location.hash + "']").parent('li').addClass('active');
                var navParent = $('.nav li.active').parent().closest('li');
                navParent.addClass("active").addClass('open');
                navParent.find('.submenu').css('display', 'block');

                $('ul.breadcrumb li:eq(1)').text($('li.active.open a:first').text());
                $('ul.breadcrumb li:eq(2)').text($('ul.submenu li.active').text());
            }
        }
    };
}]);

