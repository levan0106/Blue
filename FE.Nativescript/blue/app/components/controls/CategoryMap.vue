<template>
    <ScrollView orientation="horizontal" :width="width"
    scrollBarIndicatorVisible="false">
        <StackLayout orientation="horizontal">
            <StackLayout v-for="(item,index) in data" 
            :key="index" 
            width="80" class="category-group-item"
            @tap="onTap(item.id)">
            <!-- <label>{{item.Id}}</label> -->
                <Image :src="item.image"/>
                <Label class="category-item-title" :text="item.name"
                :class="isActive(item.id)?'actived ':''"/>
            </StackLayout>
        </StackLayout>
    </ScrollView>
</template>

<script>
export default {
    name:"CategoryMap",
    props:{
        value:{
        },
        data:{
            type:Array
        },
        multiple:{
            default:false,
            type:Boolean
        },
        width:{
            default:"100%",
            type:String
        }
    },
    computed: {
        displayValue: {
            get: function() {
                return this.value
            },
            set: function(value) {
                // Note: we cannot set this.value as it is a "prop". 
                // It needs to be passed to parent component
                // $emit the event so that parent component gets it
                this.$emit('input', value)
            }
        }
    },
    methods:{
        isActive(val){
            if(this.multiple){
                return this.displayValue.includes(val);
            }else{
                return this.displayValue == val;
            }
        },
        onTap(val){
                console.log(val);
            if(this.multiple){
                
                if(this.displayValue.includes(val)){
                    const index= this.displayValue.indexOf(val);
                    this.displayValue.splice(index,1);//remove item
                }else{
                    this.displayValue.push(val); //add item
                }
            }else{
                this.displayValue = val;
            }
        }
    }
}
</script>

<style scoped>
.category-group-item{
    padding:20px 30px;
}
.category-item-title{
    vertical-align: center;
    text-align: center;
    font-size: 14;
    color: #333333;
}
.actived{
    color:#f73486;
    font-weight: bold;
}
</style>
