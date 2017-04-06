<list-header title="Kurumsal Başvurular"></list-header>


<form id="form" class="form-inline" role="form" autocomplete="off">
    <input-select horizontal="1" label="Durum" name="Applied" model="filter.Applied" options="id as val for (id, val) in Durum"></input-select> 
    <input type="button" value="Filtrele" ng-click="search()" />
</form>

<page-size></page-size>
<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th>#</th>
            <th column-header="Tarih" field="InsertDate"></th>
            <th column-header="Adı" field="Name"></th>
            <th column-header="Yetkili Adı" field="AuthName"></th>
            <th column-header="Email" field="Email"></th>
            <th column-header="GSM No" field="PhoneCell"></th>
            <th column-header="Şehir" field="City"></th>
            <th column-header="Web URL" field="Url"></th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
            <td>{{entity.InsertDate | date:'dd-MM-yyyy'}}</td>
            <td>{{entity.Name}}</td>
            <td>{{entity.AuthName}}</td>
            <td>{{entity.Email}}</td>
            <td>{{entity.PhoneCell}}</td>
            <td>{{entity.City}}</td>
            <td>{{entity.Url}}</td>
            <td operations>
                <a ng-show="!entity.Applied" class="dtBtn red" href="/Staff/Handlers/DoCommand.ashx?method=kurumBasvuruOnayla&id={{entity.Id}}"><i class="icon-ok bigger-130" ></i></a>
                <a ng-show="entity.Applied" class="dtBtn green" href="javascript:alertNice('Bu zaten onaylandı')"><i class="icon-ok bigger-130" ></i></a>
            </td>
        </tr>
    </tbody>
</table>

<list-footer no-add-new="1"></list-footer>