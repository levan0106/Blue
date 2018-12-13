<template>
<page background="transparent">
    <StackLayout class="panel-padding home-panel modal">
        <label text="Chọn ngày thuê" class="title"></label>
        <FlexboxLayout>
            <StackLayout class="panel panel-margin date" width="50%">
                <label text="Từ ngày:"></label>
                <DatePicker v-model="from" />
            </StackLayout>
            <StackLayout class="panel panel-margin date" width="50%">
                <label text="Đến ngày:"></label>
                <DatePicker v-model="to"  />
            </StackLayout>
        </FlexboxLayout>
        <StackLayout class="panel panel-margin date-range-container"> 
            <label class="date-range">{{from|date}} - {{to|date}}</label>
            <label class="date-count">( {{caculateDay(to,from)}} ngày)</label>
        </StackLayout>
        <StackLayout>
            <button text="Đồng ý" class="btn btn-submit btn-border" @tap="$modal.close" />
            <button text="Hủy" class="btn btn-cancel" @tap="$modal.close" />
        </StackLayout>
    </StackLayout>
</page>
</template>

<script>
import formater from "@/plugin/formater"
export default {
    name:"SelectDate",
    props:{
        item:{
            default:{
            },
            style:Object
        }
    },
    data(){
        return{
            from:new Date(),
            to:new Date()
        }
    },
    methods:{
        caculateDay(from,to){
            var offset = from.getTime() - to.getTime(); // lấy độ lệch của 2 mốc thời gian, đơn vị tính là millisecond 
            var totalDays = Math.round(offset / 1000 / 60 / 60 / 24); 
           return totalDays;
        }
    }
}
</script>

<style scoped>
page{
    background: transparent;
}
.modal{
    border-radius: 40px;
}
.title{
    text-align: center;
    width: 100%;
}
.date{
    text-align: center;
    padding: 10px;
    border-right-width: 1px;
    border-left-width: 1px;
}
.date-range-container{
    padding: 10px 20px;
    margin-bottom: 40px;
    vertical-align: center;
    text-align: center;
    margin: 0 auto;
}
.date-range{
    color: #2d5bb6;
    font-size: 18px;
    font-weight: bold;
}
.btn-cancel{
    background:transparent;
    color: #2d5bb6;
}
</style>
