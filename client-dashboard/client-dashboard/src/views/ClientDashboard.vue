<template>
    <div>
        <h1>Client Dashboard</h1>
        <ClientList/>
    </div>
</template>

<script>
import ClientList from "@/components/ClientList.vue";

export default {
    name: "Dashboard",
    components: {
        ClientList
    }
};
</script>