<div class="page-header">
    <h1 class="blue">
        <i class="icon-user"></i>
        Kurum
    <small>
        <i class="icon-double-angle-right"></i>
       kurum detayları
    </small>
    </h1>
</div>

<div id="user-profile-1" class="user-profile row">
    <div class="col-xs-12 col-sm-3 center">
        <div>
            <span class="profile-picture">
                <img id="avatar" class="img-responsive" alt="Avatar" src="{{entity.LogoPath}}">
            </span>

            <div class="space-4"></div>
            <div class="width-80 label label-info label-xlg arrowed-in arrowed-in-right">
                <div class="inline position-relative">
                    <a class="user-title-label dropdown-toggle" data-toggle="dropdown">
                        <i class="icon-circle {{!entity.IsDeleted?'light-green':'red'}}"></i>
                        &nbsp;
                        <span class="white">{{entity.Name}}</span>
                    </a>

                    <ul class="align-left dropdown-menu dropdown-caret dropdown-lighter">
                        <li class="dropdown-header">İşlemler </li>

                        <li><a href="#/Edit/{{entityName}}/{{entity.Id}}"><i class="icon-pencil green"></i>&nbsp;<span class="green">Düzenle</span></a></li>
                    </ul>
                </div>
            </div>

            <div class="space-4"></div>

            <small class="block">
                <span class="orange">Kayıt Tarihi: </span>{{entity.InsertDate | date}}<br>
            </small>

            <span class="countBox">
                <i class="icon-tablet"></i>
                <span>{{entity.GsmCount | number:0}}</span>
            </span>

            <span class="countBox">
                <i class="icon-comment"></i>
                <span>{{entity.SmsCount | number:0}}</span>
            </span>

            <span class="countBox">
                <i class="icon-envelope"></i>
                <span>{{entity.EmailCount | number:0}}</span>
            </span>

        </div>

        <div class="hr hr16 dotted"></div>
    </div>

    <div class="col-xs-12 col-sm-9">

        <div class="profile-user-info profile-user-info-striped">
            <info-row label="Adı">{{entity.Name}}</info-row>
            <info-row label="Sektör">{{entity.SectorId}}</info-row>
            <info-row label="Url">{{entity.Url}}</info-row>
        </div>



        <div class="widget-box transparent" id="recent-box" ng-init="tab = 'users'">
            <div class="widget-header">
                <div class="widget-toolbar no-border">
                    <ul class="nav nav-tabs" id="recent-tab">
                        <li class="{{tab=='users' ? 'active':''}}">
                            <a ng-click="tab='users'"><i class="icon-envelope-alt red"></i>Kullanıcılar</a>
                        </li>
                        <li class="{{tab=='kampanyalar' ? 'active':''}}">
                            <a ng-click="tab='kampanyalar'"><i class="icon-envelope-alt red"></i>Kampanyalar</a>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="widget-body">
                <div class="widget-main padding-4">
                    <div class="tab-content padding-8 overflow-visible">
                            
                        <div class="tab-pane{{tab=='users' ? 'active':''}}">
                            <div ng-controller="ViewDetailListUserCompanyController" ng-init="entityName='UserCompany'; where='CompanyId = '">
                                <page-size ng-show="count/pageSize>1"></page-size>
                                <pagination ng-show="count/pageSize>1"></pagination>
                                <table class="table table-striped table-bordered table-hover dataTable" aria-describedby="table-storage_info">
                                    <tr>
                                        <th>#</th>
                                        <th column-header="Kullanıcı" field="UserName"></th>
                                        <th column-header="SMS" field="Sms"></th>
                                        <th column-header="GSM" field="Gsm"></th>
                                        <th column-header="Email" field="Email"></th>
                                    </tr>
                                    <tr ng-repeat="entity in list">
                                        <td indexer></td>
                                        <td link-to-parent="User">{{entity.UserName}}</td>
                                        <td><i class="icon-comment {{entity.Sms ? 'green':'red'}}" ng-click="toggle(entity,'Sms')" style="cursor:pointer"></i></td>
                                        <td><i class="icon-tablet {{entity.Gsm ? 'green':'red'}}" ng-click="toggle(entity,'Gsm')" style="cursor:pointer"></i></td>
                                        <td><i class="icon-envelope {{entity.Email ? 'green':'red'}}" ng-click="toggle(entity,'Email')" style="cursor:pointer"></i></td>
                                    </tr>
                                </table>
                            </div>
                        </div>


                        <div class="tab-pane{{tab=='kampanyalar' ? 'active':''}}">
                            <div ng-controller="ViewDetailListCompanyKampanyaController" ng-init="entityName='CompanyKampanya'; where='CompanyId = '">
                                <page-size ng-show="count/pageSize>1"></page-size>
                                <pagination ng-show="count/pageSize>1"></pagination>
                                <table class="table table-striped table-bordered table-hover dataTable" aria-describedby="table-storage_info">
                                    <tr>
                                        <th>#</th>
                                        <th column-header="Başlık" field="Title"></th>
                                        <th column-header="Başlangıç" field="Baslangic"></th>
                                        <th column-header="Bitiş" field="Bitis"></th>
                                        <th></th>
                                    </tr>
                                    <tr ng-repeat="entity in list">
                                        <td indexer></td>
                                        <td><a ng-click="show(entity)">{{entity.Title}}</a></td>
                                        <td>{{entity.Baslangic}}</td>
                                        <td>{{entity.Bitis}}</td>
                                        <td><a ng-click="delete(entity)">Sil</a></td>
                                    </tr>
                                </table>
                                
                                <form id="form" class="form-horizontal" role="form" autocomplete="off">
                                <div class="row">
                                    <input-text label="Başlık" model="yeni.Title" name="Title"></input-text>	
                                    <input-textarea label="Açıklama" model="yeni.Description" name="Description"></input-textarea>	
                                    <input-date label="Başlangıç" model="yeni.Baslangic" name="Baslangic"></input-date>	
                                    <input-date label="Bitiş" model="yeni.Bitis" name="Bitis"></input-date>	
                                    <div style="clear:both"></div>
                                    <input type="button" ng-click="save()" value="Kaydet"/>
                                </div>
                                </form>
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

