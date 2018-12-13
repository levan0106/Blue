<template>
    <FlexboxLayout alignItems="center" :flexWrap="flexWrap?'wrap':''"
    justifyContent="space-between" width="100%" >
        <StackLayout v-for="(item,index) in data" :key="index"
        class="btn-select-border btn-select"
        height="100px"            
        @tap="onTap(item.Key)">
            <label :text="item.Value"
            :class="isActive(item.Key)?'actived ':''"></label>
        </StackLayout>
    </FlexboxLayout>
</template>

<script>
export default {
    Name:"RadioControl",
    props:{
        value:{
        },
        data:{
            type:Array
        },
        flexWrap:{
            default:false,
            type:Boolean
        },
        multiple:{
            default:false,
            type:Boolean
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

.btn-select{
    vertical-align: middle;
    text-align: center;
    min-height: 100px;
    margin: 10px 20px;
    width: 45%;
    max-width: 200px;
}
.actived{
    color:#f73486;
    font-weight: bold;
}
</style>
