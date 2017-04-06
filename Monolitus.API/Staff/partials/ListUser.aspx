﻿<list-header title="Kullanıcılar"></list-header>


<form id="form" class="form-inline" role="form" autocomplete="off">
    <input-select no-empty-option="" horizontal="1" label="Tipi" name="UserType" model="filter.UserType" options="i.Id as i.Name for i in UserTypeList"></input-select>   
    <input-select horizontal="1" label="Durum" name="IsDeleted" model="filter.IsDeleted" options="id as val for (id, val) in Durum"></input-select> 
    <input type="button" value="Filtrele" ng-click="search()" />
</form>

<page-size></page-size>
<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th>#</th>
            <th column-header="Adı" field="Name"></th>
            <th column-header="Email Adresi" field="Email"></th>
            <th column-header="Cep tel" field="PhoneCell"></th>
            <th column-header="Facebook Link" field="FacebookId"></th>
            <th column-header="Ekleme" field="InsertDate"></th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
            <td link-to-view>{{entity.Name+' '+ entity.Surname}}</td>
            <td>{{entity.Email}}  <i class="{{entity.EmailValidated ? 'icon-ok bigger-130 green' : 'icon-remove bigger-130 red'}}"></i></td>
            <td>{{entity.PhoneCell}}  <i class="{{entity.PhoneCellValidated ? 'icon-ok bigger-130 green' : 'icon-remove bigger-130 red'}}"></i></td>
            <td><a href="https://www.facebook.com/app_scoped_user_id/{{entity.FacebookId}}" target="_blank">{{entity.FacebookId}}</a></td>
            <td>{{entity.InsertDate | date:'dd-MM-yyyy'}}</td>
            <td operations>
                <a class="dtBtn green" ng-click="login(entity)"><i class="icon-user bigger-130" title="Login"></i></a>
            </td>
        </tr>
    </tbody>
</table>

<list-footer no-add-new="1"></list-footer>