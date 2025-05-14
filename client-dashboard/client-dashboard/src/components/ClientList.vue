<template>
    <div>
        <p>This is the list of non archived clients</p>
        <div>
            <button type="button" @click="showForm = !showForm">Add New Client</button>
            <div v-show="showForm">
                <ClientForm/>
            </div>
        </div>
        <h4 v-if="!clients">No clients found</h4>
        <table v-if="clients">
            <tr>
                <th>Username</th>
                <th>Email</th>
                <th></th>
            </tr>
            <tr v-for="(client, index) in clients" :key="index">
                <td>{{ client.userName }}</td>
                <td>{{ client.email }}</td>
                <td><a :href="'/client/' + client.id">view</a></td>
            </tr>
            
        </table>
    </div>
</template>

<script>
import ClientForm from "@/components/ClientForm.vue";
import apiService from '@/services/api.service';

export default {
    name: "ClientList",
    components: {
        ClientForm
    },
    created() {
        this.getClients();
    },
    data() {
        return {
            clients: [],
            showForm: false
        }
    },
    methods: {
        async getClients() {
            const data = await apiService.get('/api/client');

            this.clients = data;
        }
    }
};
</script>

<style lang="scss">
</style>
