<list-header title="Kurumlar"></list-header>


<form id="form" class="form-inline" role="form" autocomplete="off">
    <input-select horizontal="1" label="Sektör" name="SectorId" model="filter.SectorId" options="i.Id as i.Name for i in SectorList"></input-select>           
    <input-select horizontal="1" label="Durum" name="IsDeleted" model="filter.IsDeleted" options="id as val for (id, val) in Durum"></input-select> 
    <input type="button" value="Filtrele" ng-click="search()" />
</form>

<page-size></page-size>
<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th>#</th>
            <th column-header="Logo" field="LogoPath"></th>
            <th column-header="Adı" field="Name"></th>
            <th column-header="SMS" field="SMSCount"></th>
            <th column-header="GSM" field="GSMCount"></th>
            <th column-header="Email" field="EmailCount"></th>
            <th column-header="Ekleme" field="InsertDate"></th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
            <td><img height="32" ng-src="{{entity.LogoPath}}" /></td>
            <td link-to-view>{{entity.Name}}</td>
            <td class="text-right">{{entity.SmsCount | number:0}}</td>
            <td class="text-right">{{entity.GsmCount | number:0}}</td>
            <td class="text-right">{{entity.EmailCount | number:0}}</td>
            <td>{{entity.InsertDate | date:'dd-MM-yyyy'}}</td>
            <td operations></td>
        </tr>
    </tbody>
</table>

<list-footer></list-footer>