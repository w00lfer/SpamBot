<template>
  <q-page class="flex flex-center q-pa-sm">
    <q-table
      title="SPAM EMAILS"
      :data="spam"
      :columns="columns"
      row-key="name"
      style="width: 800px; height: 600px;"
    />
  </q-page>
</template>

<script>
import { api } from 'boot/axios';

export default {
  name: 'Spam',
  data () {
    return {
      spam: [],
      columns: [
        { name: 'receiverEmail', label: 'Email', field: 'receiverEmail', sortable: true, align: 'left', style: 'width: 200px' },
        { name: 'title', label: 'Title', field: 'title', sortable: true, align: 'left', style: 'width: 200px' },
        { name: 'description', label: 'Description', field: 'description', sortable: true, align: 'left', },
      ]
    }
  },
  methods: {
    loadSpam () {
      api.get('/api/Email')
        .then((response) => {
          this.spam = response.data
        })
        .catch(() => {
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })
      })
    },
  }
}

</script>
