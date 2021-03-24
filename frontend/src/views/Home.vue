<template>
    <div class="container">
        <h1 class="mt-1">Your cryptocurrency of choice</h1>

        <div class="row">
            <div class="form-group row">
                <label class="col-4 col-form-label" for="preferredCoin">Coin:</label>
                <div class="col-8">
                    <select id="preferredCoin" name="preferredCoin" required="required" class="custom-select">
                        <option value="BTC" selected>BTC</option>
                        <option value="ETH">ETH</option>
                        <option value="XRP">XRP</option>
                    </select>
                </div>
            </div>
        </div>
    </div>
</template>