<template>
    <Page>
        <ActionBar>
            <GridLayout width="100%" columns="70, *, 50" class="search-bar">                
            <Label col="0" :text="location" class="location" @tap="onChooseLocation"/> 
            <Label col="1" class="search search-text" text="Thuê đồ chơi, đồ cũ..." 
            @tap="goToSearchPage" />
            <Label col="2" class="fa"  @tap="goToSearchPage">{{'fa-search' | fonticon}} Tìm</Label>
        </GridLayout>
        </ActionBar>   
        <GridLayout coloumns="*" rows="*,auto" >
            <ScrollView id="scrollView" row="0" col="0" 
           @scroll="onScroll($event)"
            ref="scroll" >           
                <StackLayout class="home-panel" >
                    <AbsoluteLayout top="0" left="0" class="panel">
                        <StackLayout width="100%" height="100" backgroundColor="#2d5bb6" top="0" left="0"></StackLayout>
                        <StackLayout class="banner panel-padding" top="0" left="0">
                            <Image src="~/assets/images/banner.png"/>
                            <!-- <SlideContainer loop="true" height="100%" width="100%"
                            >
                                <Slide class="slide-1">
                                            <Image src="~/assets/images/banner.png"/>
                                        </Slide>
                                        <Slide class="slide-2">
                                            <Image src="~/assets/images/banner.png"/>
                                        </Slide>
                            </SlideContainer> -->
                        </StackLayout>
                    </AbsoluteLayout>

                    <StackLayout class="category panel panel-padding">
                        <FlexboxLayout  width="100%">
                            <category-map :data="categories" v-model="category" width="75%"/>
                            <StackLayout v-for="(cat,index) in fixedCategories" 
                            :key="index"
                            width="80" class="category-group-item static-category">
                                <Image :src="cat.image" @tap="goToProductAddPage"/>
                                <Label class="category-title" :text="cat.name"/>   
                            </StackLayout>
                        </FlexboxLayout >
                    </StackLayout>

                    <StackLayout class="panel panel-padding panel-margin">
                        <item-list :data="hotTrends"></item-list>
                    </StackLayout>

                    <StackLayout class="adv">
                        <Image src="~/assets/images/banner-3.png"/>
                    </StackLayout>
                    <StackLayout class="panel panel-padding">                        
                        <item-list :data="products" list 
                        :loading="loading"
                        @loadMoreItems="fetchData"
                        :show-load-more="true"
                        :position="position"
                        :scroll="scroll"></item-list>
                    </StackLayout>

                    <StackLayout ref="endScrollView" class="adv">
                        <Image src="~/assets/images/banner-2.png"/>
                    </StackLayout>
                    <!-- <StackLayout class="panel panel-padding">                        
                        <item-list :data="products" list></item-list>
                        <label height="70"></label>
                    </StackLayout>                                -->
                    <!-- <StackLayout id="endScrollView" height="0"></StackLayout> -->
                </StackLayout>
            </ScrollView>

            <button-floating :show="showButtonAdd" label="Đăng tin"></button-floating>
            <StackLayout col="0" row="1" class="footer">
                <navigation-footer></navigation-footer>  
            </StackLayout>
        </GridLayout>
    </Page>
</template>

<script>
// import Store from '@/store/index'

import ItemList from '@/components/product/ItemList.vue'
import NavigationFooter from '@/components/navigation/NavigationFooter.vue'
import ButtonFloating from '@/components/controls/ButtonFloating.vue'
import CategoryMap from "@/components/controls/CategoryMap.vue"
import LoginPage from '@/components/login/LoginPage.vue';
// const view = require("tns-core-modules/ui/core/view");

  export default {
    name:"HomePage",
    components:{
        ItemList,
        NavigationFooter,
        ButtonFloating,
        CategoryMap,
    },
    data() {
      return {
        showButtonAdd:false,
        loading: false,
        category:null,
        //scrollTimer:0,
        position:0,
        scroll:null
      }
    },
    mounted(){
         this.scroll = this.$refs.scroll.nativeView;
        // //console.log(this.scroll);
        // if(this.scroll != null){

        // this.scroll.on("scroll", (args)=>{
        //     this.onScroll(args)
        // })
        // }
    },
    computed:{
        location(){
            return this.$store.getters['location/current'].Code
        },
        categories(){
            return this.$store.getters['category/all'] //.filter(item=>!item.fixedPosition)
        },
        fixedCategories(){
            return this.$store.getters['category/all'] //.filter(item=>item.fixedPosition)
        },
        hotTrends(){
            return this.$store.getters['product/hottrends']
        },
        products(){
            return this.$store.getters['product/all']
        },
        locations(){
            return this.$store.getters['location/all']
        }
    },
    created(){
        this.$store.dispatch('category/pullAll')
        this.$store.dispatch('product/pullAll')
        this.$store.dispatch('product/pullHotTrend')
        this.$store.dispatch('location/pullAll')
        this.$store.dispatch('location/pullCurrentLocation')
    },
    methods:{
        onScroll(args){
            //console.log("onScroll:" +args.scrollY);
            if(args.scrollY < 200){
                this.showButtonAdd =false;
            }else{
                this.showButtonAdd =true;
            }
            this.position = args.scrollY;
            // let scrollView=args.object;

            // clearTimeout(this.scrollTimer);
            // this.scrollTimer = setTimeout(()=>{
            //     this.afterScroll(args.object);
            // },1000);
        },
        // afterScroll(scroll){
        //     //console.log("Scrolling done")
        //     var scrollHeight = scroll.getActualSize().height;
        //     let endScrollView = this.$refs.endScrollView.nativeView; //view.getViewById(scroll, "endScrollView");
        //     let halfHeight = endScrollView.getActualSize().height/2;
        //     let relativeY = endScrollView.getLocationRelativeTo(scroll).y;
        //     // console.log("scrollHeight: "+scrollHeight);
        //     // console.log("halfHeight: "+halfHeight);
        //     // console.log("endScroll: "+relativeY);
        //     if(scrollHeight >= relativeY - halfHeight){
        //         this.fetchData();
        //     }
        // },
        fetchData() {
            this.loading=true
                this.$store.dispatch('product/loadMore')
                .then(response=>{
                    //setTimeout(() => {   
                    this.loading=false;
                    console.log('end fetch data')
                   // }, 500);
                })
        },
        goToSearchPage(){
            this.$router.goToSearchPage(this)
        },
        goToProductAddPage(){
            this.$router.goToProductAddPage(this);
        },
        
        onChooseLocation(){
            
            action("Chọn tỉnh thành của bạn", "Hủy", this.locations.map(item=>item.Name))
            .then(result => {
                console.log(result);
                this.$store.dispatch('location/setLocation',result);
                // var locationSelected = this.locations.filter(item=>item.Name == result)[0]
                // this.location =locationSelected.Code
            });
        },
    }
  }
</script>

<style scoped>


    .search-text{
        padding: 0 40px;
        color:#a4aecd;
        font-size: 14px;
    }
    .banner{
        width: 100%;
        padding:40px;
    }
    .banner image{
        border-radius: 20px;
        width: 100%;
        max-width: 100px;
    }
    .category-group-item{
        padding:20px 30px;
    }
    .category label{
        vertical-align: center;
        text-align: center;
        font-size: 14;
        color: #333333;
    }
    .static-category{
        padding-top: 20px;
    }
    .footer{
        background: transparent;
    }
    .location{
        border-radius: 100%;
        border-color: #92aad8;
        border-width:2px;
        vertical-align: center;
        text-align: center;
    }
    
</style>
