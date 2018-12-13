<template>
    <page>
        <ActionBar title="Yêu thích" background="transparent">
            <NavigationButton text="Quay lại" android.systemIcon="ic_menu_back" 
            @tap="$navigateBack" /> 
        </ActionBar>
        <GridLayout coloumns="*" rows="*,auto" > 
            <!-- product list -->
            <StackLayout class="panel-padding"
                row="0" col="0">    
                    <ScrollView>
                    <item-list :data="products" list></item-list>                                
                    </ScrollView>
            </StackLayout >
            <StackLayout col="0" row="1" class="footer">
                <navigation-footer tab-selected="love"></navigation-footer>  
            </StackLayout>
        </GridLayout>
    </page>
</template>

<script>

import ItemList from '@/components/product/ItemList.vue'
import NavigationFooter from '@/components/navigation/NavigationFooter.vue'
export default {
    name:"BookMarkPage",
    components:{
        ItemList,
        NavigationFooter
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

<style>

</style>
