/*!
 * Created by Jean on 8/29/2015.
 * 
 * email:mahai_1986@126.com
 *
 */
//当前目录所在的根目录在index.html所在的目录
define({
    //app name
    appName: "app"
    //controller的根目录
    //节点根路径从main.js的paths开始
    , controllerPath: "controllers/"
    //template的根目录
    , templatePath: "views/"
    //路由映射
    , route: [{
        //必填,访问的url地址
        url: "/"

        , name: "default"

        //必填
        //相对于templatePath的template地址
        , templateUrl: "default.html"

        //选填,如果缺省,则controllerUrl会根据templateUrl进行设置
        //如default.html,则生成的controllerUrl地址为default
        //再如:user/index.html,则生成结果为:user/index
        //, controllerUrl: "default"

        //选填,如果缺省,则controller会根据controllerUrl进行设置
        //controllerUrl:default --> controller:default
        //controllerUrl:user/index --> controller:index
        //, controller: "default"
    }, {
        url: "/forms"
        , templateUrl: "partial_container.html"
    }, {
        url: "/test"
        , templateUrl: "test.html"
    }]
    , formDependence: {
        "forms/test-frm1.html": ["table-form"]
    }
});