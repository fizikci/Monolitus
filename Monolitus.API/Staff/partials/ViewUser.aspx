<div class="page-header">
    <h1 class="blue">
        <i class="icon-user"></i>
        User
    <small>
        <i class="icon-double-angle-right"></i>
       details
    </small>
    </h1>
</div>

<div id="user-profile-1" class="user-profile row">
    <div class="col-xs-12 col-sm-3 center">
        <div>
            <span class="profile-picture">
                <img id="avatar" class="img-responsive" alt="Avatar" src="{{entity.Avatar || '/Assets/avatars/emp.png'}}">
            </span>

            <div class="space-4"></div>
            <div class="width-80 label label-info label-xlg arrowed-in arrowed-in-right">
                <div class="inline position-relative">
                    <a class="user-title-label dropdown-toggle" data-toggle="dropdown">
                        <i class="icon-circle {{!entity.IsDeleted?'light-green':'red'}}"></i>
                        &nbsp;
                        <span class="white">{{entity.Name+' '+entity.Surname}}</span>
                    </a>

                    <ul class="align-left dropdown-menu dropdown-caret dropdown-lighter">
                        <li class="dropdown-header">İşlemler </li>

                        <li><a href="#/Edit/{{entityName}}/{{entity.Id}}"><i class="icon-pencil green"></i>&nbsp;<span class="green">Düzenle</span></a></li>
                        <li><a ng-click="toggleDelete()"><i class="icon-trash {{!entity.IsDeleted?'red':'green'}}"></i>&nbsp;<span class="{{!entity.IsDeleted?'red':'green'}}">{{!entity.IsDeleted?'Sil':'Geri al'}}</span></a></li>
                    </ul>
                </div>
            </div>

            <div class="space-4"></div>

            <small class="block">
                <span class="orange">Signed Up: </span>{{entity.InsertDate | date}}<br>
                <span class="orange">Last Login: </span>{{entity.LastLoginDate | date}}<br>
            </small>

        </div>

        <div class="hr hr16 dotted"></div>
    </div>

    <div class="col-xs-12 col-sm-9">

        <div class="profile-user-info profile-user-info-striped">
            <div class="profile-info-row">
                <div class="profile-info-name">Name </div>

                <div class="profile-info-value">
                    {{entity.Name}}
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">Surname </div>

                <div class="profile-info-value">
                    {{entity.Surname}}
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">Email </div>

                <div class="profile-info-value">
                    <a href="mailto:{{entity.Email}}">{{entity.Email}}</a>
                </div>
            </div>
        </div>



        <div class="widget-box transparent" id="recent-box" ng-init="tab = 'Folders'">
            <div class="widget-header">
                <div class="widget-toolbar no-border">
                    <ul class="nav nav-tabs" id="recent-tab">
                        <li class="{{tab=='Folders' ? 'active':''}}">
                            <a ng-click="tab='Folders'"><i class="icon-map-marker blue"></i> Monolits</a>
                        </li>
                        <li class="{{tab=='Bookmarks' ? 'active':''}}">
                            <a ng-click="tab='Bookmarks'"><i class="icon-map-marker blue"></i> Bookmarks</a>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="widget-body">
                <div class="widget-main padding-4">
                    <div class="tab-content padding-8 overflow-visible">
                            
                        <div class="tab-pane{{tab=='Folders' ? 'active':''}}">
                            <div ng-controller="ViewDetailListController" ng-init="entityName='Folder'; where='UserId = '">
                                <page-size ng-show="count/pageSize>1"></page-size>
                                <pagination ng-show="count/pageSize>1"></pagination>
                                <table class="table table-striped table-bordered table-hover dataTable" aria-describedby="table-storage_info">
                                    <tr>
                                        <th>#</th>
                                        <th column-header="Name" field="Name"></th>
                                    </tr>
                                    <tr ng-repeat="entity in list">
                                        <td indexer></td>
                                        <td link-to-parent="Folder">{{entity.Name}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <div class="tab-pane{{tab=='Bookmarks' ? 'active':''}}">
                            <div ng-controller="ViewDetailListController" ng-init="entityName='Bookmark'; where='UserId = '">
                                <page-size ng-show="count/pageSize>1"></page-size>
                                <pagination ng-show="count/pageSize>1"></pagination>
                                <table class="table table-striped table-bordered table-hover dataTable" aria-describedby="table-storage_info">
                                    <tr>
                                        <th>#</th>
                                        <th column-header="Title" field="Title"></th>
                                        <th column-header="Url" field="Url"></th>
                                    </tr>
                                    <tr ng-repeat="entity in list">
                                        <td indexer></td>
                                        <td link-to-parent="Bookmark">{{entity.Name}}</td>
                                        <td><a href="{{entity.Url}}" target="_blank">{{entity.Url}}</a></td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            <!-- /widget-main -->
        </div>
        <!-- /widget-body -->
    </div>
</div>

