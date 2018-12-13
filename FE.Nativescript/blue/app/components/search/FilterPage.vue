<template>
    <page>
        <ActionBar title="Bộ lọc" background="#ffffff" borderColor="#2d5bb6" color="black">
            <NavigationButton text="Quay lại" android.systemIcon="ic_menu_back" 
                @tap="$navigateBack" />
            <GridLayout columns="*,auto,10" width="100%" >
                <label col="0" text="Bộ lọc"></label>
                <label col="1" text="Xoá bộ lọc"
                @tap="clearFilter"></label>
            </GridLayout>
        </ActionBar>
        <GridLayout class="panel-padding" rows="*,auto">
            <ScrollView row="0">
                <StackLayout>
                    <StackLayout class="border-bottom">
                        <label class="title">Mức giá</label>
                        <label class="price">{{price.minValue|currency}} - {{price.value|currency}}</label>
                        <label class="price-description">Mức giá trung bình là {{price.average|currency}}</label>
                        <Slider v-model="price.value" 
                        :minValue="price.minValue"
                        :maxValue="price.maxValue"
                        @valueChange="onPriceChanged" 
                        width="100%"
                        height="50"
                        color="black" borderColor="black" borderWidth="2"
                        backgroundColor="black"
                        effectiveWidth="80" effectiveHeight="80"/>
                    </StackLayout>
                    <FlexboxLayout class="border-bottom">
                        <!-- <ScrollView orientation="horizontal" width="100%"
                        scrollBarIndicatorVisible="false">
                            <StackLayout orientation="horizontal">
                                <StackLayout v-for="(cat,index) in categories" 
                                :key="index" 
                                width="80" class="category-group-item">
                                    <Image :src="cat.Image"/>
                                    <Label class="category-item-title" :text="cat.Name"/>   
                                </StackLayout>
                            </StackLayout>
                        </ScrollView> -->
                        <category-map :data="categories"
                        v-model="category" multiple/>
                    </FlexboxLayout >
                    <StackLayout class="border-bottom">
                        <label class="title">Ngày thuê</label>
                        <FlexboxLayout width="100%"
                        class="date-range">                            
                            <label width="40%" text="20/10/2018"
                            @tap="openDateRange"></label>
                            <label width="20%" text="đến"></label>
                            <label width="40%" text="20/11/2018"
                            @tap="openDateRange"></label>
                        </FlexboxLayout>
                    </StackLayout>

                    <StackLayout class="border-bottom">
                        <label class="title">Giao hàng</label>
                        <radio :data="shippings" v-model="shipping"></radio>
                    </StackLayout>

                    <StackLayout class="border-bottom">
                        <label class="title">Thông tin thêm</label>
                        <radio :data="attributes" v-model="attribute" 
                        flex-wrap multiple></radio>
                    </StackLayout>
                </StackLayout>
            </ScrollView>
            <StackLayout row="1">
                <button text="Áp dụng bộ lọc" class="btn-submit btn" @tap="applyFilter" /> 
            </StackLayout>
        </GridLayout>
    </page>
</template>

<script>
import Radio from "@/components/controls/Radio.vue"
import CategoryMap from "@/components/controls/CategoryMap.vue"
export default {
    name:"FilterPage",
    components:{
        Radio,
        CategoryMap
    },
    data(){
        return{
            category:[],
            price:{
                value:5000,
                minValue:2000,
                maxValue:6000,
                average:0
            },
            shipping:null,
            attribute:[],
            shippings:[
                {Value:"Có giao hàng", Key:1},
                {Value:"Không giao hàng", Key:2}
            ],
            attributes:[
                {Key:1,Value:"bé trai"},
                {Key:2,Value:"bé gái"},
                {Key:3,Value:"trên 90%"},
                {Key:4,Value:"chống nước"},
                {Key:5,Value:"nhựa"}
            ],
        }
    },
    computed:{
        categories(){
            return this.$store.getters['category/all'].filter(item=>!item.Static)
        },
    },
    created(){
        
        this.$store.dispatch('category/pullAll')
        this.price.average = (this.price.maxValue+this.price.minValue)/2
    },
    methods:{
        openDateRange(){
            this.$router.openSelectDateModal(this)
        },
        onPriceChanged(){

        },
        applyFilter(){
            this.$navigateBack()
        },
        clearFilter(){
            this.$navigateBack()
        }
    }
}
</script>

<style scoped>
.border-bottom{
    margin-bottom: 20px;
}
.date-range{
    text-align: center;
}
/* .actived{
    color: #2d5bb6;
    font-weight: bold;
    vertical-align: center;
    text-align: center;
} */
/* .btn-select{
    vertical-align: middle;
    text-align: center;
    min-height: 100px;
    margin: 10px 20px;
    width: 45%;
    max-width: 200px;
} */
/* .category-group-item{
    padding:20px 30px;
}
.category-item-title{
    vertical-align: center;
    text-align: center;
    font-size: 14;
    color: #333333;
} */
.price{
    font-weight: bold;
    margin: 15px 0;
}
.price-description{
    font-size:12px;
    margin: 15px 0;
}
</style>
