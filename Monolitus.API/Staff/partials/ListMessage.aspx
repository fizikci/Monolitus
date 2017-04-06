<list-header title="Mesajlar"></list-header>

<page-size></page-size>
<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th>#</th>
            <th column-header="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tarih&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" field="InsertDate"></th>
            <th column-header="Gönderen" field="Email"></th>
            <th column-header="Konu" field="Subject"></th>
            <th column-header="Mesaj" field="Message"></th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
            <td>{{entity.InsertDate | date}}</td>
            <td ng-if="entity.UserId"><a href="#/View/User/{{entity.UserId}}">{{entity.UserName}}</a></td>
            <td ng-if="!entity.UserId">{{entity.Email}}</td>
            <td>{{entity.Subject}}</td>
            <td>{{entity.MessageText}}</td>
            <td operations></td>
        </tr>
    </tbody>
</table>

<list-footer no-add-new="1"></list-footer>