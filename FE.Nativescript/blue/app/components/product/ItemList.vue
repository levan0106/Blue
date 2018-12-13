<template>
<StackLayout>
    <StackLayout v-if="list" class="item-list">
        <StackLayout v-for="(item, index) in data" :key="index" 
        @tap="goToDetailPage(item)"> 
            <FlexboxLayout flexDirection="row" class="list-group-item"
                @tap="goToDetailPage(item)">
                <StackLayout>
                    <Image :src="item.image" width="200" height="100"/>
                    </StackLayout>
                    <StackLayout  class="item-detail">
                        <label :text="item.name" class="name"></label>
                        <label :text="item.price|currency" class="price"></label>
                        <label :text="item.availableDate" class="date"></label>
                        <FlexboxLayout alignItems="center" 
                        justifyContent="space-between" width="100%" >
                            <label horizontalAlignment="left" :text="item.location" class="location" dock="left"></label>
                            <label horizontalAlignment="right" v-show="item.shipping != null" :text="item.shipping" class="shipping" dock="right" width="70"></label>
                        </FlexboxLayout>
                    </StackLayout>
            </FlexboxLayout>
        </StackLayout >

        <!-- <ListView for="item in data"  
        :height="data.length * rowHeight" 
        :rowHeight="rowHeight"
        @loadMoreItems="loadMoreItems($event)"
        @loaded="onListViewLoaded($event)">
            <v-template>
                <FlexboxLayout flexDirection="row" class="list-group-item"
                @tap="goToDetailPage(item)">
                    <StackLayout width="250">
                        <Image :src="item.image"/>
                    </StackLayout>
                    <StackLayout  class="item-detail">
                        <label :text="item.name" class="name"></label>
                        <label :text="item.price|currency" class="price"></label>
                        <label :text="item.availableDate" class="date"></label>
                        <FlexboxLayout  alignItems="center" 
                        justifyContent="space-between" width="100%" >
                            <label horizontalAlignment="left" :text="item.location" class="location" dock="left"></label>
                            <label horizontalAlignment="right" v-show="item.shipping != null" :text="item.shipping" class="shipping" dock="right" width="70"></label>
                        </FlexboxLayout  >
                    </StackLayout>
                </FlexboxLayout>
            </v-template>
        </ListView> -->
    </StackLayout> 

    <WrapLayout v-else class="item-card" >
        <StackLayout  v-for="(item, index) in data" :key="index" width="50%"
        class="item-detail" @tap="goToDetailPage(item)">
            <Image :src="item.image"/>
            <label :text="item.name" class="name"></label>
            <label :text="item.price|currency" class="price"></label>
            <label :text="item.availableDate" class="date"></label>
            <DockLayout stretchLastChild="false" width="100%">
                <label :text="item.location" class="location" dock="left" ></label>
                <label v-show="item.shipping != null" :text="item.shipping" 
                class="shipping" dock="right" width="70"></label>
            </DockLayout  >
        </StackLayout>
        <StackLayout v-if="data.length>0 && showLoadMore" class="more">
            <label text="Xem thêm"></label>
        </StackLayout>
    </WrapLayout> 
    
    <ActivityIndicator v-if="showLoadMore" :busy="true" :class="loading?'spinner':'hidden'"  />
    <StackLayout v-if="showLoadMore" ref="endScrollView" height="0"></StackLayout> 
    </StackLayout>
</template>

<script>
export default {
    name:'ItemList',
    props:{
        list:{
            default:false,
            type:Boolean
        },
        data:{
            type:Array
        },
        // rowHeight:{
        //     default:100,
        //     type:Number
        // },
        loading:{
            default:false,
            type:Boolean
        },
        showLoadMore:{
            default:false,
            type:Boolean
        },
        scroll:{
            require:true,
            type:Object
        },
        position:{
            type:Number
        }
    },
    data(){
        return{
            isBusy:false,
            scrollTimer:0
        }
    },
    watch:{
        position(val){
            this.onScroll();
        }
    },
    mounted(){
        //let scroll = this.$refs.scroll.nativeView;
        //console.log(scroll);
        // if(scroll != null){

        // scroll.on("scroll",this.onScroll)
        // }
    },
    methods:{
        onScroll(){
            //console.log("scrolling")
            clearTimeout(this.scrollTimer);
            this.scrollTimer = setTimeout(()=>{
                this.afterScroll(this.scroll);
            },1000);
        },
        afterScroll(scroll){
            var scrollHeight = scroll.getActualSize().height;
            let endScrollView = this.$refs.endScrollView.nativeView;
            let halfHeight = endScrollView.getActualSize().height/2;
            let relative = endScrollView.getLocationRelativeTo(scroll);
            // console.log("scrollHeight: "+scrollHeight);
            // console.log("halfHeight: "+halfHeight);
            // console.log("endScroll: "+relative);
            
            if(relative !=null && scrollHeight >= relative.y - halfHeight){
                this.loadMoreItems();
            }
        },
        
        loadMoreItems(args){
           // console.log('load more')
            //alert('load more');
            this.$emit("loadMoreItems");
            this.isBusy=true;
        },
        onListViewLoaded(args){
            console.log('loaded')
            this.isBusy=false;
        },
        goToDetailPage(obj){
            this.$router.goToProductDetailPage(this,obj)
        },
    }
}
</script>

<style scoped>
.hidden{
    display: none;
}
.item-detail{
    padding: 20px;
}
Image{
    height: 100%;
    width: 100%;
    border-radius: 20px!important;
}
.name{
    font-size: 18px;
    font-weight: bold;
}   
.price{
    color:#f73486;
    font-weight: bold;
}
.date, .location,.shipping{
    vertical-align: center;
    font-size: 11px;
    line-height: 60px;
    height: 60px;
}
.shipping{
    border-radius: 100%;
    border-color: #22a803;
    border-width:2px;
    padding: 5px 20px;
    color:#22a803;
    vertical-align: center;
    text-align: center;
}
.more{
    vertical-align: center;
    text-align: center;
    color:#2d5bb6;
    width: 100%;
    height: 100px;;
}
</style>
