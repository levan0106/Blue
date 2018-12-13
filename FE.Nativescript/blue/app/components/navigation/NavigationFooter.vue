<template>
  
    <StackLayout class="navigation">
        <WrapLayout width="100%">
            <StackLayout v-for="(item,index) in items" :key="index" width="20%"
            @tap="goTo(item.action)">
                <Image :src="item.image"/>
                <Label :text="item.name" :class="item.key == tabSelected?'selected':''"/>   
            </StackLayout>
        </WrapLayout>
    </StackLayout>
</template>

<script>
export default {
    name:"NavigationFooter",
    props:{
        tabSelected:{
            type:String
        }
    },
    data(){
        return{
            items:[
                {
                    key:"search",
                    image:"~/assets/images/navigator1.png",
                    name:"Tìm kiếm",
                    action:"SearchPage"
                },
                {
                    key:"basket",
                    image:"~/assets/images/navigator2.png",
                    name:"Giỏ hàng",
                    action:"BasketPage"
                },
                {
                    key:"love",
                    image:"~/assets/images/navigator3.png",
                    name:"Yêu thích",
                    action:"BookMarkPage"
                },
                {
                    key:"message",
                    image:"~/assets/images/navigator4.png",
                    name:"Tin nhắn",
                    action:"MessagePage"
                },
                {
                    key:"profile",
                    image:"~/assets/images/navigator5.png",
                    name:"Cá nhân",
                    action:"ProfilePage"
                }
            ]
        }
    },
    methods:{
        goToSearchPage(){
            this.$router.goToSearchPage(this)
        },
        goTo(action){
            this.$router.goTo(this,action)
        }
    }
}
</script>

<style scoped>
.selected{
    color: #2d5bb6;
    font-weight: bold;
}
.navigation{
    padding:10px 20px;
    border-top-width:1px; 
    border-color:#666666;    
    vertical-align: center;
    text-align: center;
    font-size: 12px;
    background: #ffffff;
}
image{
    height: 100px;
}
.btn-add{
    vertical-align: center; 
    text-align: center;
    background: red;
}
.btn-add Image{
    border-radius: 100%;
    width: 70;
}
</style>
