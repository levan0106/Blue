<template>
    <Page actionBarHidden="true" backgroundSpanUnderStatusBar="true">
        <ActionBar title="Chi tiết" background="transparent">
            <NavigationButton text="Quay lại" android.systemIcon="ic_menu_back" 
            @tap="$navigateBack" /> 
            <ActionItem @tap="onTapEdit"
                v-show="!isEditing"
                ios.systemIcon="2" ios.position="right"
                android.systemIcon="ic_menu_edit" />
            <ActionItem @tap="onTapSave"
                v-show="isEditing"
                ios.systemIcon="3" ios.position="right"
                android.systemIcon="ic_menu_save" />
            <ActionItem @tap="onTapCancel"
                v-show="isEditing"
                ios.systemIcon="1"
                android.systemIcon="ic_menu_close_clear_cancel" />
        </ActionBar>
        <GridLayout  rows="*,auto" >
            <ScrollView row="0" @scroll="onScroll($event)" >
                <StackLayout>
                    <AbsoluteLayout>
                        <Image left="0" top="0" :src="data.product.image" 
                        class="image" width="100%"/>
                        <action-bar back-label="<" left="0" top="0" background="transparent"/>                        
                    </AbsoluteLayout>

                    <StackLayout class="panel-padding ">
                        <GridLayout columns="60,*,20"  class="author-info border-bottom">
                            <Image col="0" :src="data.author.avatar" width="60" height="60" borderRadius="100%" />
                            <StackLayout col="1" padding="20px" 
                            @tap="goToSellerPage(data.author.name)">
                                <label :text="data.author.name" fontSize="16px" fontWeight="Bold"></label>
                                <label :text="data.product.date"></label>
                            </StackLayout>
                            <label col="2" text=">" fontSize="22px" height="80px"></label>
                        </GridLayout >
                    </StackLayout>

                    <StackLayout class="panel-padding product-info">
                        <StackLayout class="border-bottom">
                            <label :text="data.product.name" class="name"></label>
                            <label :text="data.product.availableDate" class="date"></label>
                            <DockLayout stretchLastChild="false" >
                                <label :text="data.product.address" class="address" dock="left"></label>
                                <label v-show="data.product.shipping != null" :text="data.product.shipping" class="shipping" dock="right" 
                                width="100"></label>
                            </DockLayout  >
                        </StackLayout>
                        <StackLayout class="border-bottom">
                            <label text="Thông tin thêm" class="title title-small"></label>
                            <FlexboxLayout flexWrap="wrap" 
                            alignItems="center" width="100%" >
                                <StackLayout class="btn-select-border btn-select"
                                v-for="(item,index) in attributes" :key="index">
                                    <label :text="item.value" ></label>
                                </StackLayout>                            
                            </FlexboxLayout>
                        </StackLayout>
                        <StackLayout class="border-bottom">
                            <label text="Mô tả" class="title title-small"></label>
                            <label  class="address" textWrap="true">
                                <FormattedString>
                                    <span :text="data.product.description"></span>
                                </FormattedString>
                            </label>
                        </StackLayout>
                    </StackLayout>                     
                    <StackLayout class="panel-padding">
                        <label text="Gợi ý" class="title"></label>
                        <item-list :data="products"></item-list>
                    </StackLayout>                   
                </StackLayout>
            </ScrollView>

            <StackLayout row="1" class="footer" >
                <StackLayout class="panel-padding">
                    <GridLayout columns="*,auto">
                        <StackLayout col="0">
                            <label class="price">{{data.product.price|currency}}</label>
                            <label class="deposit-price">{{data.product.deposit|currency}} (Tiền cọc)</label>
                        </StackLayout>
                        <button col="1" text="THUÊ NGAY" class="btn-submit btn" @tap="goToSelectDate(data.product)" />
                    </GridLayout>
                </StackLayout>
                <StackLayout ref="navigationFooter" :height="nagativePosition">
                    <navigation-footer></navigation-footer> 
                </StackLayout> 
            </StackLayout>
        </GridLayout>
    </Page>
</template>

<script>

// import Store from '@/store/index'
import ItemList from '@/components/product/ItemList.vue'
import NavigationFooter from '@/components/navigation/NavigationFooter.vue'
import ActionBar from '@/components/controls/ActionBar.vue'
// import { Observable, Scheduler } from "rxjs";
// const timeElapsed = Observable.defer(() => {
//     const start = Scheduler.animationFrame.now();
//     return Observable.interval(1)
//         .map(() => Math.floor((Date.now() - start)));
// });

// const duration = (totalMs) =>
//     totalMs
//         .map(elapsedMs => elapsedMs - 10)
//         .takeWhile(t => t <= 1);

// const amount = (d) => (t) => t * d;

// const elasticOut = (t) =>
//     Math.sin(-13.0 * (t + 1.0) *
//         Math.PI / 2) *
//     Math.pow(2.0, -10.0 * t) +
//     1.0;

export default {
    name:"ProductDetailPage",
    components:{
        ItemList,
        NavigationFooter,
        ActionBar      
    },
    props:{
        id:{
            type:String
        },
        item:{
            default:{
            },
            style:Object
        }
    },
    data(){
        return{
            data:{
                author:{
                    avatar:"~/assets/images/c-add.png",
                    name:"Tung Le"
                },
                product:this.item
            },
            isEditing:false,
            attributes:[
                {
                    id:1,
                    value:"bé trai"
                },
                {
                    id:3,
                    value:"trên 90%"
                },
                {
                    id:4,
                    value:"chống nước"
                },
                {
                    id:5,
                    value:"nhựa"
                }
            ],
            nagativePosition:0,
            scrollTimer:0,
            intervalTimer:0
        }
    },
    computed:{
        products(){
            return this.$store.getters['product/related']
        }
    },
    created(){
        this.$store.dispatch('product/pullRelated',this.item.id)
    },
    methods:{
        goToSellerPage(id){
            this.$router.goToSellerPage(this,id)
        },
        onScroll(args){
            //console.log("onScroll:" +args.scrollY);
            if(args.scrollY < 100 ){
                    clearTimeout(this.scrollTimer);
                    this.scrollTimer = setTimeout(()=>{
                        this.intervalTimer =  setInterval(()=>{
                                if(this.nagativePosition > 0){
                                    this.nagativePosition -= 2;
                                }else{                    
                                    clearInterval(this.intervalTimer);
                                }
                            },1);
                    },500);
                     
                //this.showButtonAdd =false;
                // let navigationFooter = this.$refs.navigationFooter.nativeView;
                // navigationFooter.animate({
                //     duration: 3000,
                //     curve: AnimationCurve.easeInOut,
                //     translate: { x: 100, y: 100}
                // })
            }else{
                //this.showButtonAdd =true;
                    clearTimeout(this.scrollTimer);
                    this.scrollTimer = setTimeout(()=>{
                        this.intervalTimer = setInterval(()=>{
                            if(this.nagativePosition < 60){
                                this.nagativePosition += 2;
                            }else{
                                
                                clearInterval(this.intervalTimer);
                            }
                        },1);
                    },500);
            }
        },
        goToSelectDate(item){
            this.$router.openSelectDateModal(this)
        },
        onTapEdit(){
            this.isEditing=true;
        },
        onTapSave(){
            this.isEditing=false;
        },
        onTapCancel(){
            this.isEditing=false;
        }
    }
}
</script>

<style scoped>
.view {
    animation-name: translate;
    animation-duration: 3;
}
@keyframes translate {
    from { height: 0; }
    to { height: 100; }
}

.btn-select{
    vertical-align: middle;
    text-align: center;
    min-height: 90px;
    margin: 10px;
}
.author-info{
    /* padding-bottom:0px!important ; */
    vertical-align: center;
}
.footer{
    border-top-color: #666666;
    border-top-width:1px;
}
.more-info{
    border-radius: 100%;
    border-color: #666666;
    border-width:2px;
    padding: 5px 20px;
    color:#666666;
    vertical-align: center;
    text-align: center;
    line-height: 100px;
    /* height: 80px;*/
    margin: 10px;
    width: 50%;
}
.name{
    font-size: 22px;
    font-weight: bold;
    padding-bottom: 30px;
}   
.price{
    color:#f73486;
    font-weight: bold;
    font-size: 18px;
    /* margin-left: 20px; */
}
.deposit-price{
    /* font-weight: bold; */
    /* margin-left: 20px; */
}
.date, .address,.shipping{
    vertical-align: center;
    font-size: 13px;
    /* line-height: 80px; */
    /* height: 80px; */
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
</style>
