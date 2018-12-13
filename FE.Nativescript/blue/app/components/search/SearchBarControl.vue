<template>
<GridLayout columns="*,50">
    <GridLayout columns="70,*" class="search-bar-container" >   
        <StackLayout col="0" class="location-container" height="100%">
            <Label :text="location" class="location1" @tap="onChooseLocation" />
        </StackLayout>
        <TextField col="1" class="search" hint="Gõ để bắt đầu tìm kiếm"
            returnKeyType="search"
            @tap="onFocus"
            @returnPress="onSearchSubmit" 
            @textChange="onTextChange"
            v-model="searchPhrase" />
        <!-- <SearchBar col="1" class="search"
        hint="Gõ để bắt đầu tìm kiếm"  @textChange="onFocus" @submit="onSearchSubmit"
        textFieldHintColor="#ffffff" /> -->
    </GridLayout>
    
    <label col="2" class="cancel" @tap="goToHomePage" text="Hủy" ></label>
</GridLayout>
</template>

<script>
// import Store from '@/store/index'
export default {
    name:"SearchBarControl",
    props:{
        home:{
            default:false,
            type:Boolean
        },
        value:{}
    },
    data(){
        return{
            //searchPhrase:this.text
        }
    },
    computed:{     
        location(){
            return this.$store.getters['location/current'].Code
        },   
        locations(){
            return this.$store.getters['location/all']
        },
        searchPhrase: {
            get: function() {
                return this.value
            },
            set: function(value) {
                this.$emit('input', value)
            }
        }
    },
    created(){
        this.$store.dispatch('location/pullAll')
    },
    watch:{
        text(val){
            //console.log(val)
            this.searchPhrase=val;
        }
    },
    methods:{
        goToHomePage(){
            this.$router.goToHomePage(this)
        },
        onChooseLocation(){
            action("Chọn tỉnh thành của bạn", "Hủy", this.locations.map(item=>item.Name))
            .then(result => {
                this.$store.dispatch('location/setLocation',result);
            });
        },
        
        onSearchSubmit(){
            //console.log("onSearchSubmit")
            this.$store.dispatch('search/showPanel',false)
            this.$emit("search",this.searchPhrase)
            //this.$store.dispatch('product/search',this.searchPhrase)
        },
        onFocus(){
            console.log("onFocus")
            this.$store.dispatch('search/showPanel',true)
        },
        onBlur(){
            console.log("onBlur")
            this.$store.dispatch('search/showPanel',false)
        },
        onTextChange(){

        }
    }
}
</script>

<style scoped>
.search-bar-container,
.location-container{    
    border-radius: 100%;
    border-color: #92aad8;
    border-width:2px;
    height: 100%;
}
.location-container{
    background-color: #92aad8;
    vertical-align: middle;
}
.search-text{
    padding: 0 20px;
    color:#a4aecd;
    font-size: 14px;
}

.cancel,.location1{
    vertical-align: center;
    text-align: center;
}
</style>
