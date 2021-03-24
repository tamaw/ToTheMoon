<template>
    <div class="container">
        <PreferredCoin/>
        <CoinOverview/>
    </div>
</template>
<script>
import CoinOverview from '../components/CoinOverview'
import PreferredCoin from '../components/PreferredCoin'

export default {
    components: {CoinOverview, PreferredCoin}
}
</script>
