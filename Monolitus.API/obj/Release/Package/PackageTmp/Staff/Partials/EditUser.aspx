    <div class="page-header">
        <h1>Kullanıcı
            <small>
                <i class="icon-double-angle-right"></i>
                düzenle
            </small>
        </h1>
    </div>

<form id="form" class="form-horizontal" role="form" autocomplete="off">

<input type="text" ng-model="entity.Id" name="Id" style="display:none"/>

<div class="row">
    <div class="col-sm-9">
		
        <input-text label="Adı" model="entity.Name" name="Name"></input-text>		
        <input-text label="Soyadı" model="entity.Surname" name="Surname"></input-text>       		
        <input-select label="Cinsiyet" name="Gender" model="entity.Gender" options="i.Id for i in EnumCinsiyet"></input-select> 		
		
        <div class="form-group">
            <label for="DateOfBirth" class="col-sm-3 control-label no-padding-right"> Doğum Tarihi </label>
            <div class="col-sm-9">
				
                <input type="text" class="date-picker" ng-model="entity.DogumTarihi" name="DogumTarihi" data-date-format="dd-mm-yyyy" />
				<span class="input-group-addon">
					<i class="icon-calendar bigger-110"></i>
				</span>
		
            </div>
        </div>
    
        <div class="space-4"></div>			
        <input-textarea label="Hakkımda" model="entity.About" name="About"></input-textarea>
        <input-text label="Identity No" model="entity.Certificates" name="Certificates"></input-text>        
        <input-number label="Order No" model="entity.OrderNo" name="OrderNo"></input-number>	       
        <input-file label="Resim" model="entity.Avatar" name="Avatar"></input-file>
	</div>
    <div class="col-sm-3">
        <div class="col-sm-3">
            <img src="{{entity.Avatar}}" />
        </div>
	</div>
</div>

<div class="row">
	
	<div class="col-xs-12 col-sm-6">
		<h3 class="header smaller lighter blue">Contact Info</h3>
		
        <input-text label="PhoneCell" model="entity.PhoneCell" name="PhoneCell"></input-text>
        <input-text label="PhoneHome" model="entity.PhoneHome" name="PhoneHome"></input-text>
        <input-text label="PhoneWork" model="entity.PhoneWork" name="PhoneWork"></input-text>
        <input-textarea label="AddressLine1" model="entity.AddressLine1" name="AddressLine1"></input-textarea>
        <input-textarea label="AddressLine2" model="entity.AddressLine2" name="AddressLine2"></input-textarea>
        <input-text label="Web" model="entity.Web" name="Web"></input-text>
        <input-text label="ZipCode" model="entity.ZipCode" name="ZipCode"></input-text>
        <input-text label="City" model="entity.City" name="City"></input-text>  
		
	</div>
	
	<div class="col-xs-12 col-sm-6">
		<h3 class="header smaller lighter blue">Account Info</h3>
		
        <input-text label="Email" model="entity.Email" name="Email"></input-text>
        <input-text label="Password" model="entity.Password" name="Password"></input-text>
        <input-text label="Takma Adı" model="entity.Nick" name="Nick"></input-text>        
        <input-select label="Ülkesi" name="Ulke" model="entity.UlkeId" options="i.Id as i.Name for i in Countries"></input-select>
        <input-select label="Tercih Ettiği Dil" name="PrefferedLang" model="entity.PrefferedLang" options="i.Id as i.Name for i in EnumLang"></input-select>    
        <!--<input-select label="UserType" name="UserType" model="entity.UserType" options="i.Id as i.Name for i in EnumUserTypes"></input-select>-->  	
		
	</div>
	
	<div class="col-xs-12 col-sm-6">
		<h3 class="header smaller lighter blue">Extra Info</h3>
        <input-text label="Occupation" model="entity.Occupation" name="Occupation"></input-text>
        <input-text label="Company" model="entity.Company" name="Company"></input-text>
        <input-text label="Department" model="entity.Department" name="Department"></input-text>
        <input-text label="Education" model="entity.Education" name="Education"></input-text>  
	     
		
	</div>
	
</div>

<div class="clearfix form-actions">
	<div class="text-right">
		<button class="btn btn-xs btn-primary" type="button" ng-click="save()">
			<i class="icon-ok bigger-110"></i>
			Save
		</button>
		&nbsp; 
		<button class="btn btn-xs btn-info" type="button" onclick="history.go(-1)">
			<i class="icon-undo bigger-110"></i>
			Cancel
		</button>
	</div>
</div>

</form>
