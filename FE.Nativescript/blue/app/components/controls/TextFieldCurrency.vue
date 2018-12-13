<template>
    <GridLayout columns="*,auto" class="container">
        <TextField col="0" :hint="hint" keyboardType="number" :returnKeyType="returnKeyType"
        v-model="displayValue"
        @blur="isInputActive = false" 
        @focus="isInputActive = true" 
        width="100%"
        class="textfield"/>
        <label col="1" :text="currency" class="label" ></label>        
     </GridLayout>
</template>

<script>
export default {
    name:"TextFieldCurrency",
    props: {
        hint:{
            type:String
        },
        value:{
        },
        digit:{
            type:Number,
            default:0
        },
        currency:{
            type:String,
            default:'VNĐ'
        },
        disabled:{
            type:Boolean,
            default:false
        },
        currencyPosition:{
            type:String,
            default:'right'
        },
        returnKeyType:{
            type:String,
            default:'next'
        }
    },
    data: function() {
        return {
            isInputActive: false,
        }
    },
    computed: {
        displayValue: {
            get: function() {
                if (this.isInputActive) {
                    // Cursor is inside the input field. unformat display value for user
                    return this.value == null || isNaN(parseFloat(this.value))? 0: this.value.toString()
                } else {
                    // User is not modifying now. Format display value for user interface
                    let value = isNaN(parseFloat(this.value))? null : parseFloat(this.value).toFixed(this.digit).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                    return value;
                    if(value == null)
                        return null
                        
                    if(this.currencyPosition === 'left'){
                        return this.currency + value
                    }else{
                        return value + " " + this.currency
                    }
                    
                }
            },
            set: function(modifiedValue) {
                // Recalculate value after ignoring "$" and "," in user input
                let value = modifiedValue
                if(typeof(value) == 'string' && modifiedValue.startsWith('0'))
                    value =  modifiedValue.substring(1,modifiedValue.length)
                    
                let newValue = typeof(value) == 'number'? value : parseFloat(value.replace(/[^0-9.-]/g, ""))
                // Ensure that it is not NaN
                if (isNaN(newValue)) {
                    newValue = 0
                }
                // Note: we cannot set this.value as it is a "prop". 
                // It needs to be passed to parent component
                // $emit the event so that parent component gets it
                this.$emit('input', newValue)
            }
        }
    }
}
</script>
<style scoped>
.container,
.textfield{
    border-bottom-color: #ffffff;
    border-bottom-width:1px;
}
.container{
    border-bottom-color: #666666;
    border-bottom-width:1px;
    vertical-align: center;
}
.label{
    margin-top:30px; 
}
</style>

