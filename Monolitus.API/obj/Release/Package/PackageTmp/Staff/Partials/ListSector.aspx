<list-header title="Sektörler"></list-header>


<page-size></page-size>
<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th>#</th>
            <th column-header="Adı" field="Name"></th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
            <td>{{entity.Name}}</td>
            <td operations></td>
        </tr>
    </tbody>
</table>

<list-footer></list-footer>