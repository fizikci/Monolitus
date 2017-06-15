<list-header title="Users"></list-header>


<form id="form" class="form-inline" role="form" autocomplete="off">
    <input-select no-empty-option="" horizontal="1" label="Type" name="UserType" model="filter.UserType" options="i.Id as i.Name for i in UserTypeList"></input-select>   
    <input-select horizontal="1" label="State" name="IsDeleted" model="filter.IsDeleted" options="id as val for (id, val) in Durum"></input-select> 
    <input type="button" value="Filter" ng-click="search()" />
</form>

<page-size></page-size>
<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th>#</th>
            <th column-header="Name" field="Name"></th>
            <th column-header="Email" field="Email"></th>
            <th column-header="Facebook Link" field="FacebookId"></th>
            <th column-header="Added" field="InsertDate"></th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
            <td link-to-view>{{entity.Name}}</td>
            <td>{{entity.Email}}  <i class="{{entity.EmailValidated ? 'icon-ok bigger-130 green' : 'icon-remove bigger-130 red'}}"></i></td>
            <td><a href="https://www.facebook.com/app_scoped_user_id/{{entity.FacebookId}}" target="_blank">{{entity.FacebookId}}</a></td>
            <td>{{entity.InsertDate | date:'dd-MM-yyyy'}}</td>
            <td operations></td>
        </tr>
    </tbody>
</table>

<list-footer no-add-new="1"></list-footer>