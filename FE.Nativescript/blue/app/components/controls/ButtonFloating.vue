<template>     
    <StackLayout v-show="show" class="float-btn-container">
        <StackLayout class="btn-add" @tap="goToProductAddPage" >
            <Image src="~/assets/images/c-add.png"/>
            <Label :text="label"/>
        </StackLayout>  
    </StackLayout>  
</template>

<script>

// import LoginPage from '@/components/login/LoginPage.vue'

export default {
    name:"ButtonFloating",
    props:{
        show:{
            default:false,
            type:Boolean
        },
        label:{
            default:"Thêm",
            type:String
        }
    },
    methods:{
        goToProductAddPage(){
            // this.$navigateTo(LoginPage,{
            //  transition: {
            //     name:"slide",
            //     duration: 200,
            //     curve: 'linear'
            // },
            // transitionIOS: {},
            // transitionAndroid: {
            //     name:"slide",
            //     duration: 200,
            //     curve: 'linear'
            // },
            
            // props: {
            //     item:""
            // }
            // });
            this.$router.goToProductAddPage(this)
        }
    }
}
</script>

<style scoped>
    .btn-add{
        vertical-align: center; 
        text-align: center;
        background: transparent;
        box-shadow: 10px 5px 5px black;
    }
    .btn-add label{
        background: #ffffff;
        border-radius: 100%;
        width: 70;
    }
    .btn-add Image{
        border-radius: 100%;
        width: 50;
    }
    .float-btn-container{
        margin-left: 0;
        margin-top: 78%;
        color:#2d5bb6;
    }
</style>
