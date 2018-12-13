<template>
    <page>
        <ActionBar title="Shop" background="transparent">
            <NavigationButton text="Quay lại" android.systemIcon="ic_menu_back" 
            @tap="$navigateBack" /> 
        </ActionBar>
        <GridLayout coloumns="*" rows="*,auto" > 
            <!-- product list -->
            <StackLayout
                row="0" col="0">    
                    <ScrollView class="home-panel">
                        <StackLayout>
                            <AbsoluteLayout>                                
                                <Image src="~/assets/images/banner.png" 
                                    height="170" top="0" left="0"/>
                                <StackLayout class="header-panel" top="100" left="0"
                                    height="130" width="100%">
                                    <StackLayout class="header-container"                                                                  
                                    backgroundColor="#ffffff" height="100%" width="100%" >                                
                                        <label class="title">{{id}}</label>
                                    </StackLayout>
                                </StackLayout>                                
                            </AbsoluteLayout>
                            <StackLayout  class="panel panel-padding panel-margin">
                                <item-list :data="products"></item-list>    
                            </StackLayout>
                        </StackLayout>                            
                    </ScrollView>
            </StackLayout >
            <StackLayout col="0" row="1" class="footer">
                <navigation-footer></navigation-footer>  
            </StackLayout>
        </GridLayout>
    </page>
</template>

<script>

import ItemList from '@/components/product/ItemList.vue'
import NavigationFooter from '@/components/navigation/NavigationFooter.vue'
export default {
    name:"SellerPage",
    components:{
        ItemList,
        NavigationFooter
    },
    props:{
        id:{
            type:String
        }
    },
    computed:{        
        products(){
            return this.$store.getters['product/all']
        }
    },
    created(){        
        this.$store.dispatch('product/pullAll')
    },
}
</script>

<style scoped>
.header-container{
    width: 100%;
    padding:40px;
    border-radius: 20px;
    text-align: center;
    vertical-align: center;
}
.header-panel{
    padding:0px 40px;
    background: transparent;
}
</style>
