<list-header title="Arama Sonuçları"></list-header>
<page-size></page-size>
<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th>#</th>
            <th column-header="Varlık" field="EntityName"></th>
            <th column-header="Adı" field="Name"></th>
            <th column-header="Kayıt T." field="InsertDate"></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
            <td>{{entity.EntityName}}</td>
            <td><a href="#/View/{{entity.EntityName}}/{{entity.EntityId}}">{{entity.Name}}</a></td>
            <td>{{entity.InsertDate | date:'yyyy-MM-dd'}}</td>
        </tr>
    </tbody>
</table>


<style>
    table i {
        cursor:pointer
    }
</style>