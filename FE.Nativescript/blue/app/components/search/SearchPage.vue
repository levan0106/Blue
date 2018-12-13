<template>
    <page>
        <ActionBar>
            <search-bar-control v-model="searchPhrase"
            @search="onSearch"></search-bar-control>
        </ActionBar> 
        <GridLayout coloumns="*" rows="*,auto" > 
            <StackLayout v-if="showSearchPanel" class="search-panel panel-padding" 
                height="100%"  row="0" col="0">
                <StackLayout class="panel-padding">
                    <FlexboxLayout justifyContent="space-between"
                    width="100%" alignItems="center">
                        <label horizontalAlignment="left" text="Gợi ý" class="title"></label>
                        <label horizontalAlignment="right" text="x" class="btn-close"
                        @tap="closeSuggestion"></label>
                    </FlexboxLayout>
                    <ListView for="item in suggestions" @itemTap="onSelect"
                    class="suggestion-panel"
                    :height="suggestions.length * rowHeight"
                    :rowHeight="rowHeight-2">
                        <v-template>
                            <FlexboxLayout flexDirection="row" class="list-group-item">
                                <Label :text="item.label" class="search-item-list"/>
                            </FlexboxLayout>
                        </v-template>
                    </ListView>
                </StackLayout>   
                <StackLayout class="panel-padding">
                    <label text="Lịch sử tìm kiếm" class="title"></label>
                    <ListView for="item in histories" @itemTap="onSelect"
                    :height="histories.length * rowHeight"
                    :rowHeight="rowHeight-2">
                        <v-template>
                            <FlexboxLayout flexDirection="row" class="list-group-item">
                                <Label :text="item.label" class="search-item-list"/>
                            </FlexboxLayout>
                        </v-template>
                    </ListView>
                </StackLayout>  
            </StackLayout>
            
            <!-- product list -->
            <StackLayout v-else class="search-result panel-padding"
                row="0" col="0"> 
                    <FlexboxLayout alignItems="center" 
                    justifyContent="space-between" width="100%" 
                    class="result-header">
                        <label horizontalAlignment="left" >{{products.length}} kết quả</label>
                        <FlexboxLayout horizontalAlignment="right">
                            <label class="btn-select-border item-header" @tap="onOrderBy" >
                                <span>Xếp theo: </span>
                                <span class="order" :text="orderBy">Mới nhất</span>
                            </label>
                            <label  class="btn-select-border btn-select-filter item-header" @tap="goToFilterPage">Bộ lọc</label>
                        </FlexboxLayout>
                    </FlexboxLayout>     
                    <ScrollView ref="scroll" @scroll="onScroll($event)">
                        <item-list :data="products"
                        :scroll="scroll"
                        :position="position"
                        :loading="loading"
                        show-load-more
                        @loadMoreItems="fetchData"></item-list>                                
                    </ScrollView>
            </StackLayout >
            <StackLayout v-if="!showSearchPanel" col="0" row="1" class="footer">
                <navigation-footer tab-selected="search"></navigation-footer>  
            </StackLayout>
        </GridLayout>
    </page>
</template>

<script>
import SearchBarControl from '@/components/search/SearchBarControl.vue'
import ItemList from '@/components/product/ItemList.vue'
import NavigationFooter from '@/components/navigation/NavigationFooter.vue'

export default {
    name:"SearchPage",
    components:{
        SearchBarControl,
        ItemList,
        NavigationFooter
    },
    data(){
        return{
            rowHeight:40,
            searchPhrase:"",
            orderBy:'Mới nhất',
            position:0,
            loading:false,
            scroll:null
        }
    },
    computed:{        
        products(){
            return this.$store.getters['product/search']
        },
        histories(){
            return this.$store.getters['search/histories']
        },
        suggestions(){
            return this.$store.getters['search/suggestions']
        },
        showSearchPanel(){
            return this.$store.getters['search/showPanel']
        }
    },
    mounted(){
         this.scroll = this.$refs.scroll.nativeView;
         this.$store.dispatch('search/showPanel',true)
    },
    created(){        
        this.$store.dispatch('search/pullHistories')
        this.$store.dispatch('search/pullSuggestions')
        this.$store.dispatch('search/showPanel',false)
        this.$store.dispatch('product/search','')
    },
    methods:{
        goToFilterPage(){
            this.$router.goToFilterPage(this)
        },
        onSelect(event){
            //console.log(event.item.label)
            this.searchPhrase=event.item.label           
        },
        closeSuggestion(){
            this.$store.dispatch('search/showPanel',false)
        },
        onOrderBy(){
            action("Sắp xếp theo", "Bỏ qua", ["Mới nhất", "Giá giảm dần","Giá tăng dần"])
            .then(result => {
                //console.log(result);
                if(result !='Bỏ qua'){
                    this.orderBy=result;
                }
            });
        },
        onScroll(args){
            this.position = args.scrollY;
        },
        fetchData() {
            this.loading=true
            this.$store.dispatch('product/searchLoadMore',this.searchPhrase)
            .then(response=>{
                setTimeout(() => {   
                    this.loading=false;
                    console.log('end fetch data')
                }, 500);
            })
        },
        onSearch(val){
            this.$store.dispatch('product/search',this.searchPhrase)
        }
    }
}
</script>

<style scoped>
.item-header{
    padding: 20px 40px;
}
.btn-close{
    border-radius: 100%;
    border-color: #92aad8;
    border-width:2px;
    vertical-align: center;
    text-align: center;
    line-height: 80px;
    width: 80px;
    height: 80px
}
.result-header{
    margin-bottom: 20px;
    padding: 20px;
}
.order,
.btn-select-filter{
    color: #f73486;
}
.suggestion-panel{
    min-height: 100%;
    vertical-align: center;
}
.search-result{
    /* margin-top: 20px; */
}
.search-panel{
    /* background: #ffffff; */
    width: 100%;
}
.search-item-list{
}
.list-group-item{
    /* height: 150px; */
    vertical-align: center;
    padding: 20px;
}
</style>
