<template>
    <AbsoluteLayout height="50" 
        width="100%">
        <StackLayout width="100%" height="100%" 
        class="background" top="0" left="0"></StackLayout>
        <GridLayout top="0" left="0" columns="auto,*,auto" 
        width="100%" class="action-bar">
            <label col="0"
            :text="backLabel" 
            @tap="$navigateBack" 
            class="back"/>
            <label col="2" text="..."></label>
        </GridLayout>
    </AbsoluteLayout>
</template>

<script>
export default {
    name:"ActionBar",
    props:{
        title:{
            type:String
        },
        backLabel:{
            default:"Quay lại",
            type:String
        }
    }
}
</script>

<style scoped>
    .action-bar {
        color: #ffffff;
        background: transparent;
        vertical-align: center;
        text-align: center;
    }
    .background{
        /* background: black; */
        opacity: 0.4;
        background-image: linear-gradient(black, #a3a3a3);
    }
    .back{
    }
    label{
        padding: 20px;
        font-size: 30px;
        vertical-align: center;
    }
</style>
