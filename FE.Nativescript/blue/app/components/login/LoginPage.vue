<template>
    <page actionBarHidden="true" backgroundSpanUnderStatusBar="true">
        <AbsoluteLayout class="body">
            <Image src="~/assets/images/login.png" top="0" left="0" width="100%"/>
            
            <ActivityIndicator :busy="isBusy" @busyChange="onBusyChanged"
             class="spinner" />
            <FlexboxLayout class="btn-container" width="100%">
                <button text="Đăng nhập bằng Facebook" class="btn-fb btn" @tap="doLogin" />                
                <button text="Đăng nhập bằng Google" class="btn-google btn" @tap="doLogin" />
            </FlexboxLayout>
        </AbsoluteLayout>
    </page>
</template>

<script>

// import Store from '@/store/index'
import * as ApplicationSettings from "application-settings";

export default {
    name:"LoginPage",
    props:{
        returnUrl:{
            type:String
        }
    },
    data(){
        return{
            isBusy:false
        }
    },
    mounted(){
        // this.$store.subscribe((mutations, state) => {
        //     ApplicationSettings.setString("store", JSON.stringify(state));
        // });
    },
    methods:{
        onBusyChanged(){

        },
        doLogin(val,event){
            this.isBusy =true
            this.$store.dispatch('login/doLogin',{user:"tungle",password:"123"})
            .then(response=>{
                var status = this.$store.getters['login/status'];           
                var token = this.$store.getters['login/token'];  
                var refreshToken = this.$store.getters['login/refreshToken'];  
                // console.log('status:'+ status)
                // console.log('token:'+ token)
                this.isBusy =false;

                //save login info
                this.$store.commit('save',{
                    isAuthenticated : response.status === 200 && response.token != '',
                    currentUser : {
                        name:'Tung Le'
                    },
                    token : response.token,
                    refreshToken : response.refreshToken
                })
                if(response.status != 200 || response.token == ''){
                    alert('Login failed !!!')
                        .then(() => {
                            console.log("Alert dialog closed.");
                            //this.$router.goToProductAddPage(this,'login')
                        });
                }else{
                    this.$router.goTo(this,this.returnUrl)
                }
            })
            .catch(e=>{
                console.log(e)
            })
        }
    }
}
</script>

<style scoped>
.body{
    background-color: #fff1f6;
}
.btn-container{
    margin-top: 73%;
}
.spinner{
    margin-top: 20%;
    margin-left: 45%;
}
.btn{
    width: 100%;
    color: #ffffff;
    border-radius: 100%;
    min-height: 120px;
    height: 100%;
    margin: 10px 0;
    font-size: 10px;
    padding: 0px 20px;
    margin: 0px 20px;
}
.btn-fb{
    background: #2d5bb6;
    
}
.btn-google{
    background: #d0256d;
    
}
</style>
