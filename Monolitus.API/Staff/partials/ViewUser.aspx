<div class="page-header">
    <h1 class="blue">
        <i class="icon-user"></i>
        Kullanıcı
    <small>
        <i class="icon-double-angle-right"></i>
       kullanıcı detayları
    </small>
    </h1>
</div>

<div id="user-profile-1" class="user-profile row">
    <div class="col-xs-12 col-sm-3 center">
        <div>
            <span class="profile-picture">
                <img id="avatar" class="img-responsive" alt="Avatar" src="/Assets/avatars/emp.png">
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
                        <li><a ng-click="toggleBultenAlimi()"><i class="icon-ok {{entity.BultenAlimi?'red':'green'}}"></i>&nbsp;<span class="{{entity.BultenAlimi?'red':'green'}}">{{entity.BultenAlimi?'Bülten Alma':'Bülten Al'}}</span></a></li>
                        <li><a ng-click="toggleDelete()"><i class="icon-trash {{!entity.IsDeleted?'red':'green'}}"></i>&nbsp;<span class="{{!entity.IsDeleted?'red':'green'}}">{{!entity.IsDeleted?'Sil':'Geri al'}}</span></a></li>
                    </ul>
                </div>
            </div>

            <div class="space-4"></div>

            <small class="block">
                <span class="orange">Nick: </span>{{entity.Nick}}<br>
                <span class="orange">Kayıt Tarihi: </span>{{entity.InsertDate | date}}<br>
                <span class="orange">Son Giriş Tarihi: </span>{{entity.LastLoginDate | date}}<br>
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
            <div class="profile-info-row">
                <div class="profile-info-name">Birth Date </div>

                <div class="profile-info-value">
                    {{entity.DogumTarihi | date}}
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">Gender </div>

                <div class="profile-info-value">
                    {{entity.Gender}}
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">Ülke </div>

                <div class="profile-info-value">
                    {{entity.Ulke}}
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">Company </div>

                <div class="profile-info-value">
                    <span>{{entity.Company }}</span>
                    <span ng-if="entity.Department" class="orange">Department:</span> {{entity.Department }}
                    <span ng-if="entity.Education" class="orange">Education:</span> {{entity.Education }}
                    <span ng-if="entity.About" class="orange">About:</span> {{entity.About }}
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">Adress </div>

                <div class="profile-info-value">
                    <span>{{entity.City }}</span>
                    <span ng-if="entity.AddressLine1" class="orange">AddressLine1:</span> {{entity.AddressLine1 }}
                    <span ng-if="entity.AddressLine2" class="orange">AddressLine2:</span> {{entity.AddressLine2 }}
                    <span ng-if="entity.ZipCode" class="orange">ZipCode:</span> {{entity.ZipCode }}
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">Phone </div>

                <div class="profile-info-value">
                    <span>{{entity.PhoneNumber }}</span>
                    <span ng-if="entity.FaxNumber" class="orange">Fax:</span> {{entity.FaxNumber }}
                    <span ng-if="entity.GsmPhoneNumber" class="orange">Mobile:</span> {{entity.GsmPhoneNumber }}
                </div>
            </div>
            <div class="profile-info-row">
                <div class="profile-info-name">Order No </div>

                <div class="profile-info-value">
                    &nbsp;{{entity.OrderNo }}
                </div>
            </div>
        </div>



        <div class="widget-box transparent" id="recent-box" ng-init="tab = 'Companies'">
            <div class="widget-header">
                <div class="widget-toolbar no-border">
                    <ul class="nav nav-tabs" id="recent-tab">
                        <li class="{{tab=='Companies' ? 'active':''}}">
                            <a ng-click="tab='Companies'"><i class="icon-map-marker blue"></i>İzinler</a>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="widget-body">
                <div class="widget-main padding-4">
                    <div class="tab-content padding-8 overflow-visible">
                            
                        <div class="tab-pane{{tab=='Companies' ? 'active':''}}">
                            <div ng-controller="ViewDetailListUserCompanyController" ng-init="entityName='UserCompany'; where='UserId = '">
                                <page-size ng-show="count/pageSize>1"></page-size>
                                <pagination ng-show="count/pageSize>1"></pagination>
                                <table class="table table-striped table-bordered table-hover dataTable" aria-describedby="table-storage_info">
                                    <tr>
                                        <th>#</th>
                                        <th column-header="Company" field="CompanyName"></th>
                                        <th column-header="SMS" field="Sms"></th>
                                        <th column-header="GSM" field="Gsm"></th>
                                        <th column-header="Email" field="Email"></th>
                                    </tr>
                                    <tr ng-repeat="entity in list">
                                        <td indexer></td>
                                        <td link-to-parent="Company">{{entity.CompanyName}}</td>
                                        <td><i class="icon-comment {{entity.Sms ? 'green':'red'}}" ng-click="toggle(entity,'Sms')" style="cursor:pointer"></i></td>
                                        <td><i class="icon-tablet {{entity.Gsm ? 'green':'red'}}" ng-click="toggle(entity,'Gsm')" style="cursor:pointer"></i></td>
                                        <td><i class="icon-envelope {{entity.Email ? 'green':'red'}}" ng-click="toggle(entity,'Email')" style="cursor:pointer"></i></td>
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

