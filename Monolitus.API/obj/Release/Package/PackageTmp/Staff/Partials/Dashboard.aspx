<div class="page-header">
    <h1>Dashboard
            <small>
                <i class="icon-double-angle-right"></i>
                genel bakış & istatistikler
            </small>
    </h1>
</div>

<div class="col-sm-12" style="margin-bottom:20px">
<div class="widget-box">
    <div class="widget-header widget-header-flat widget-header-small">
        <h5>
            <i class="icon-signal"></i>
            Üye olan kişi sayısı
        </h5>

        <div class="widget-toolbar no-border">
            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                {{ehbmSel}}
				<i class="icon-angle-down icon-on-right bigger-110"></i>
            </button>

            <ul class="dropdown-menu pull-right dropdown-125 dropdown-lighter dropdown-caret">
                <li><a ng-click="showReport('UsersForLastWeek', 'Bu Hafta')">&nbsp;Bu Hafta</a></li>
                <li><a ng-click="showReport('UsersForLastMonth', 'Bu Ay')">&nbsp;Bu Ay</a></li>
                <li><a ng-click="showReport('UsersForLastYear', 'Bu Yıl')">&nbsp;Bu Yıl</a></li>
                <li><a ng-click="showReport('UsersForAll', 'Tümü')">&nbsp;Tümü</a></li>
            </ul>
        </div>
    </div>

    <div class="widget-body">
        <div class="widget-main">

            <style>
                .demo-container {
                    box-sizing: border-box;
                    width: 100%;
                    height: 250px;
                    padding: 20px 15px 15px 15px;
                }

                .chart {
                    font-size: 14px;
                    line-height: 1.2em;
                    display: none;
                    width: 100%;
                    height: 200px;
                }
            </style>

            <div class="demo-container">
                <div class="chart"
                    time-chart="UsersForAll"
                    interval="month"
                    background-color="#1D242D">
                </div>
            </div>


        </div>
    </div>
</div>
</div>
