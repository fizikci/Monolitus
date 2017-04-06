    <div class="page-header">
        <h1>Kurum
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
		
        <input-select label="Sektör" name="Sector" model="entity.SectorId" options="i.Id as i.Name for i in Sectors"></input-select>
        <input-text label="Adı" model="entity.Name" name="Name"></input-text>	
        <input-check label="Öne Çıkar" model="entity.OneCikar" name="OneCikar"></input-check>
        <input-textarea label="Açıklama" model="entity.Description" name="Description"></input-textarea>
        <input-file label="Logo" model="entity.LogoPath" name="LogoPath"></input-file>
        <input-file label="Büyük Resim" model="entity.CoverPicture" name="CoverPicture"></input-file>
        <input-text label="Url" model="entity.Url" name="Url"></input-text>

        <fieldset>
            <legend>Kısa No</legend>
            <input-check label="Kısa No" model="entity.KisaNo" name="KisaNo"></input-check>
            <input-text label="Keyword" model="entity.KisaNoKeyword" name="KisaNoKeyword"></input-text>
            <input-text label="UserName" model="entity.KisaNoUserName" name="KisaNoUserName"></input-text>
            <input-text label="Password" model="entity.KisaNoPassword" name="KisaNoPassword"></input-text>
        </fieldset>
        
        <fieldset>
            <legend>API Linkleri</legend>
            <input-text label="Sms Allow Link" model="entity.ApiSmsAllowLink" name="ApiSmsAllowLink"></input-text>
            <input-text label="Sms Reject Link" model="entity.ApiSmsRejectLink" name="ApiSmsRejectLink"></input-text>
            <input-text label="Gsm Allow Link" model="entity.ApiGsmAllowLink" name="ApiGsmAllowLink"></input-text>
            <input-text label="Gsm Reject Link" model="entity.ApiGsmRejectLink" name="ApiGsmRejectLink"></input-text>
            <input-text label="Email Allow Link" model="entity.ApiEmailAllowLink" name="ApiEmailAllowLink"></input-text>
            <input-text label="Email Reject Link" model="entity.ApiEmailRejectLink" name="ApiEmailRejectLink"></input-text>
            <hr/>
            <input-text label="Email Change Link" model="entity.ApiEmailChangeLink" name="ApiEmailChangeLink"></input-text>
            <input-text label="Phone Change Link" model="entity.ApiPhoneChangeLink" name="ApiPhoneChangeLink"></input-text>
        </fieldset>
	</div>
    <div class="col-sm-3">
        <div class="col-sm-3">
            <img src="{{entity.LogoPath}}" />
        </div>
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
