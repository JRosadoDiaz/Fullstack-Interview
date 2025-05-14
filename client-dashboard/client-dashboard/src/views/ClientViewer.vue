<template>
    <div>
        <h1>Client Viewer</h1>
        <div v-if="clientData">
            <h2 v-if="clientData.archived">This client is archived</h2>
            <div>
                <div>
                    <h4>Username</h4>
                    <div v-if="editClient">
                        <input type="text" v-model="editedClientData.userName" />
                    </div>
                    <div v-else>
                        <p>{{ clientData.userName }}</p>
                    </div>
                </div>
                <div>
                    <h4>Email</h4>
                    <div v-if="editClient">
                        <input type="text" v-model="editedClientData.email" />
                    </div>
                    <div v-else>
                        <p>{{ clientData.email }}</p>
                    </div>
                </div>
                <button type="button" @click="editClient = !editClient">{{ editClient ? 'Cancel' : 'Edit' }}</button>
                <button v-if="editClient" type="button" @click="saveClient">Save</button>
                <button v-show="!editClient" type="button" @click="archiveClient">Archive</button>
                <p v-if="message">{{ message }}</p>
                <p v-if="error">{{ error }}</p>
            </div>
            <PhoneList :clientId="clientId"/>
        </div>
    </div>
</template>

<script>
import PhoneList from "@/components/PhoneList.vue";
import apiService from "@/services/api.service.js"

export default {
    name: "ClientViewer",
    components: {
        PhoneList
    },
    data() {
        return {
            clientData: null,
            clientId: null,
            error: null,
            message: null,
            editClient: false,
            editedClientData: {
                userName: '',
                email: '',
            }
        }
    },
    created() {
        const id = this.$route.params.id;
        if (id) {
            this.clientId = id;
            this.getClient(id);
        } else {
            this.error = new Error("Client ID could not be found");
        }
    },
    watch: {
        clientData(newVal) {
            if (newVal) {
                this.editedClientData = { ...newVal };
            }
        },
        editClient(newVal) {
            if (!newVal && this.clientData) {
                this.editedClientData = { ...this.clientData };
            }
        }
    },
    methods: {
        validateForm() {
            if (this.editedClientData.userName == this.clientData.userName &&
                this.editedClientData.email == this.clientData.email) {
                    return false;
            }
            if (this.editedClientData.userName == '' || this.editedClientData.email == '') {
                return false;
            }

            return true;
        },
        async getClient(id) {
            const url = `/api/client/${id}`;
            try {
                this.clientData = await apiService.get(url);
            } catch (err) {
                this.error = err;
                this.clientData = null;
            }
        },
        async archiveClient() {
            // open confirmation window
            if(confirm("Do you really want to archive?")) {
                const url = `/api/client/archive/${this.clientData.id}`
                try {
                    await apiService.put(url);
                    // redirect back to dashboard
                    this.$router.push('/');
                } catch (err) {
                    this.error = err;
                }
            }
        },
        async saveClient() {
            if (this.validateForm()) {
                const url = `/api/client/${this.clientData.id}`;
                const body = this.editedClientData;
                try {
                    await apiService.put(url, body);
                    this.editClient = false;
                    this.clientData.userName = this.editedClientData.userName;
                    this.clientData.email = this.editedClientData.email;
                    this.message = "Client saved successfully";
                } catch (err) {
                    this.error = err;
                }
            }
        }
    }
};
</script>

<style lang="scss">

td {
    p {
        margin-top: 4px;
        margin-bottom: 4px;
        text-align: center;
    }
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}
</style>